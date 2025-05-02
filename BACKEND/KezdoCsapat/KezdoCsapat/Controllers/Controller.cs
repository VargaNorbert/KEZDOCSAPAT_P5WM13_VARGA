using KezdoCsapat.Models;
using Microsoft.AspNetCore.Mvc;

namespace KezdoCsapat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        [HttpPost]
        public IActionResult PostPlayers([FromBody] List<Player> players)
        {
            if (players == null || players.Count == 0)
            {
                return BadRequest("No players received.");
            }

            // Validate each player has Name and Position
            if (players.Any(p => string.IsNullOrWhiteSpace(p.Name) || string.IsNullOrWhiteSpace(p.Position)))
            {
                return BadRequest("Every player must have a Name and Position.");
            }

            // Check count between 11 and 15
            if (players.Count < 11 || players.Count > 15)
            {
                return BadRequest($"The number of players must be between 11 and 15. Received: {players.Count}");
            }

            // You now have a valid List<Player>
            // For now, just return them back as a confirmation
            return Ok(new
            {
                Message = $"{players.Count} players successfully received.",
                Players = players
            });
        }
    }
}
