document.getElementById('generateBtn').addEventListener('click', function () {
    const fileInput = document.getElementById('jsonFile');
    const messageDiv = document.getElementById('message');
    messageDiv.textContent = '';

    if (fileInput.files.length === 0) {
        showMessage('Kérlek válassz ki egy JSON fájlt!', 'danger');
        return;
    }

    const file = fileInput.files[0];
    const reader = new FileReader();
    reader.onload = function (e) {
        try {
            const jsonData = JSON.parse(e.target.result);

            if (!Array.isArray(jsonData)) {
                showMessage('Hiba: A JSON nem egy tömb!', 'danger');
                return;
            }

            const playerCount = jsonData.length;
            if (playerCount <= 11 || playerCount >= 15) {
                showMessage(`Hiba: A játékosok száma ${playerCount}, de 11 és 15 között kell lennie!`, 'danger');
                return;
            }

            const allValid = jsonData.every(player =>
                player.hasOwnProperty('Name') && player.hasOwnProperty('Position')
            );

            if (!allValid) {
                showMessage('Hiba: Minden játékosnak tartalmaznia kell Name és Position mezõt!', 'danger');
                return;
            }

            fetch('http://localhost:5000/api/players', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(jsonData)
            })
                .then(response => {
                    if (!response.ok) throw new Error('A szerver hibát jelzett');
                    return response.json();
                })
                .then(data => {
                    console.log('Válasz a szervertõl:', data);
                    showMessage('Sikeres küldés a szerverre!', 'success');
                    renderFormations(data.formations);
                })
                .catch(error => {
                    console.error('Hiba a küldés közben:', error);
                    showMessage('Hiba a szerverrel való kommunikáció közben!', 'danger');
                });

        } catch (error) {
            showMessage('Hiba: Nem érvényes JSON fájl!', 'danger');
        }
    };
    reader.readAsText(file);
});

function showMessage(message, type) {
    const messageDiv = document.getElementById('message');
    messageDiv.innerHTML = `<div class="alert alert-${type}" role="alert">${message}</div>`;
}

function renderFormations(formations) {
    const container = document.getElementById('formationsContainer');
    container.innerHTML = '';

    formations.forEach(f => {
        const card = document.createElement('div');
        card.className = 'card mb-3';
        card.innerHTML = `
            <div class="card-body">
                <h5 class="card-title">${f.formationName}</h5>
                <p><strong>Goodness:</strong> ${f.goodness}%</p>
                <p><strong>GK:</strong> ${f.gk.map(p => p.name).join(', ')}</p>
                <p><strong>DF:</strong> ${f.df.map(p => p.name).join(', ')}</p>
                <p><strong>MF:</strong> ${f.mf.map(p => p.name).join(', ')}</p>
                <p><strong>FW:</strong> ${f.fw.map(p => p.name).join(', ')}</p>
                <p><strong>SUB:</strong> ${f.sub.map(p => p.name).join(', ')}</p>
            </div>
        `;
        container.appendChild(card);
    });
}
