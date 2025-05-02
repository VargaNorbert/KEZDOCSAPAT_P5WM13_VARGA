using Microsoft.AspNetCore.Mvc;
using KezdoCsapat.Models;

namespace KezdoCsapat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormationController : ControllerBase
    {
        [HttpPost("generate")]
        public IActionResult GenerateFormations([FromBody] List<Player> players)
        {
            var results = new List<FormationResult>
            {
                new FormationResult {
                    FormationName = "4-4-2",
                    StartingEleven = players.Take(11).ToList(),
                    GoodnessScore = 90.5
                },
                new FormationResult {
                    FormationName = "4-3-3",
                    StartingEleven = players.Take(11).ToList(),
                    GoodnessScore = 75.3
                },
                new FormationResult {
                    FormationName = "3-5-2",
                    StartingEleven = players.Take(11).ToList(),
                    GoodnessScore = 60.0
                }
            };

            return Ok(results);
        }
    }
}