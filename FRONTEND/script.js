async function generateFormations() {
    const dummyPlayers = [
        { name: "Játékos 1", position: "GK" },
        { name: "Játékos 2", position: "DF" },
        { name: "Játékos 3", position: "DF" },
        { name: "Játékos 4", position: "DF" },
        { name: "Játékos 5", position: "DF" },
        { name: "Játékos 6", position: "MF" },
        { name: "Játékos 7", position: "MF" },
        { name: "Játékos 8", position: "MF" },
        { name: "Játékos 9", position: "FW" },
        { name: "Játékos 10", position: "FW" },
        { name: "Játékos 11", position: "FW" }
    ];

    const response = await fetch('http://localhost:5000/api/formation/generate', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(dummyPlayers)
    });

    const data = await response.json();
    displayResults(data);
}

function getCardColor(score) {
    if (score >= 80) return "bg-success text-white";
    if (score >= 60) return "bg-warning";
    return "bg-danger text-white";
}

function displayResults(results) {
    const container = document.getElementById("results");
    container.innerHTML = "";

    results.forEach(result => {
        const div = document.createElement("div");
        div.className = "col-md-4 mb-3";

        div.innerHTML = `
            <div class="card ${getCardColor(result.goodnessScore)}">
                <div class="card-body">
                    <h5 class="card-title">${result.FormationName}</h5>
                    <p class="card-text">Jóság: ${result.goodnessScore.toFixed(1)}</p>
                    <ul>
                        ${result.startingEleven.map(p => `<li>${p.name} - ${p.position}</li>`).join("")}
                    </ul>
                </div>
            </div>
        `;

        container.appendChild(div);
    });
}
