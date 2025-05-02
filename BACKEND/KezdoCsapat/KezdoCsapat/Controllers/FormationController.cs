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
            // Example fixed response
            var response = new List<Formation>
            {
                new Formation { FormationName = "4-4-2", StartingEleven = players.Take(11).ToList() },
                new Formation { FormationName = "4-3-3", StartingEleven = players.Take(11).ToList() }
            };

            return Ok(response);
        }
    }
}