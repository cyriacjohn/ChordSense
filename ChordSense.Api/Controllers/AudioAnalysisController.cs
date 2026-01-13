using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ChordSense.Api.Controllers
{

    [ApiController]
    [Route("api/analysis")]
    public class AudioAnalysisController : ControllerBase
    {
        [HttpPost("audio")]
        public async Task<IActionResult> AnalyzeAudio([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No audio file uploaded");
            }
            using var client = new HttpClient();
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);

            content.Add(fileContent, "file", file.FileName);

            var response = await client.PostAsync("http://localhost:5001/analyze/audio", content);
            var result = await response.Content.ReadAsStringAsync();

            return Ok(result);
        }
    }
}

