using KezdoCsapat.Models;
using Microsoft.AspNetCore.Mvc;

namespace KezdoCsapat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly string[] formations = { "442", "433", "532", "343", "352", "361", "451", "541" };

        [HttpPost]
        public IActionResult PostPlayers([FromBody] List<Player> players)
        {
            var formationsList = new List<Formation>();

            foreach (var form in formations)
            {
                var formation = BuildFormation(form, players);
                formationsList.Add(formation);
            }

            formationsList = formationsList.OrderByDescending(f => f.goodness).ToList();

            return Ok(new
            {formations = formationsList});
        }

        private Formation BuildFormation(string formationName, List<Player> players)
        {
            var formation = new Formation { FormationName = $"{formationName[0]}-{formationName[1]}-{formationName[2]}"};

            int gkCount = 1;
            int dfCount = int.Parse(formationName[0].ToString());
            int mfCount = int.Parse(formationName[1].ToString());
            int fwCount = int.Parse(formationName[2].ToString());

            var availablePlayers = new List<Player>(players);

            formation.GK = TakePlayers(availablePlayers, "GK", gkCount);
            formation.DF = TakePlayers(availablePlayers, "DF", dfCount);
            formation.MF = TakePlayers(availablePlayers, "MF", mfCount);
            formation.FW = TakePlayers(availablePlayers, "FW", fwCount);

            int selectedCount = formation.GK.Count + formation.DF.Count + formation.MF.Count + formation.FW.Count;

            formation.SUB = availablePlayers.Take(players.Count - selectedCount).ToList();

            double totalNeeded = gkCount + dfCount + mfCount + fwCount;
            double correctlyFilled =
                formation.GK.Count(p => p.Position == "GK") +
                formation.DF.Count(p => p.Position == "DF") +
                formation.MF.Count(p => p.Position == "MF") +
                formation.FW.Count(p => p.Position == "FW");

            formation.goodness = Math.Round((correctlyFilled / totalNeeded) * 100, 2);

            return formation;
        }

        private List<Player> TakePlayers(List<Player> players, string position, int count)
        {
            var selected = players.Where(p => p.Position == position).Take(count).ToList();

            if (selected.Count < count)
            {
                var needed = count - selected.Count;
                var fillers = players.Except(selected).Take(needed).ToList();
                selected.AddRange(fillers);
            }

            foreach (var player in selected)
                players.Remove(player);

            return selected;
        }
    }
}
