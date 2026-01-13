using Microsoft.AspNetCore.Mvc;
using ChordSense.Api.Models.Requests;
using ChordSense.Api.Models.Responses;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChordSense.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnalysisController : ControllerBase
    {
        [HttpPost("lyrics")]
        public IActionResult AnalyzeLyrics([FromBody] LyricAnalysisRequest request)
        {
            var result = new LyricAnalysisResponse
            {
                WesternKey = "C Major",
                WesternChords = new[] { "C", "G", "Am", "F" },
                CarnaticRaga = "Shankarabharanam",
                HindustanRaga = "Bilawal",
                Swaras = new[] { "Sa", "Ri2", "Ga3", "Ma1", "Pa", "Dha2", "Ni3" }
            };

            return Ok(result);
        }
    }
}
