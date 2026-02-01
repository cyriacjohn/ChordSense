using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ChordSense.Api.Controllers
{

    [ApiController]
    [Route("api/analysis")]
    public class AudioAnalysisController : ControllerBase
    {
        private readonly HttpClient _http;

        public AudioAnalysisController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
        }


        [HttpPost("audio")]
        public async Task<IActionResult> AnalyzeAudio([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No audio file uploaded");
            }
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(file.ContentType);

            content.Add(fileContent, "file", file.FileName);

            var response = await _http.PostAsync("http://localhost:5001/analyze/audio", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            return Content(responseBody, "application/json");
        }
    }
}

