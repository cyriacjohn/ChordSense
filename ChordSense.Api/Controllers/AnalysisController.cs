using Microsoft.AspNetCore.Mvc;
using ChordSense.Api.Models.Requests;
using ChordSense.Api.Models.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;
using System.Text.Json;

[ApiController]
[Route("api/analysis")]
public class AnalysisController : ControllerBase
{
    private readonly HttpClient _http;

    public AnalysisController(IHttpClientFactory httpClientFactory)
    {
        _http = httpClientFactory.CreateClient();
    }

    [HttpPost("lyrics")]
    public async Task<IActionResult> AnalyzeLyrics([FromBody] LyricAnalysisRequest request)
    {
        var flaskUrl = "http://localhost:5001/analyze/lyrics";
        var payload = new
        {
            text = request.Lyrics
        };

        var json = JsonSerializer.Serialize(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _http.PostAsync(flaskUrl, content);
        var result = await response.Content.ReadAsStringAsync();

        return Content(result, "application/json");
    }
}

