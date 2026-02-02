using Microsoft.AspNetCore.Mvc;
using ChordSense.Api.Models.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;
using System.Text.Json;
using System.Net.Http.Headers;
using ChordSense.Api.Data;
using ChordSense.Api.Models;
using ChordSense.Api.Models.Requests;
using System.Net.Http.Json;
using ChordSense.Api.DTOs;

[ApiController]
[Route("api/analysis")]
public class AnalysisController : ControllerBase
{
    private readonly HttpClient _http;
    private readonly ILogger<AnalysisController> _logger;
    private readonly ChordSenseDbContext _db;

    public AnalysisController(HttpClient httpClient, ILogger<AnalysisController> logger, ChordSenseDbContext db)
    {
        _http = httpClient;
        _logger = logger;
        _db = db;
    }

    [HttpPost("lyrics")]
    public async Task<IActionResult> AnalyzeLyrics([FromBody] LyricAnalysisRequest request)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Lyrics))
            {
                _logger.LogWarning("Lyrics request received with empty body");
                return BadRequest("Lyrics cannot be empty");
            }

            var flaskUrl = "http://localhost:5001/analyze/lyrics";

            var response = await _http.PostAsJsonAsync(flaskUrl, new { lyrics = request.Lyrics });

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Flask AI returned error. StatusCode: {StatusCode}", response.StatusCode);
                return StatusCode((int)response.StatusCode, "Python NLP service error");
            }

            var result = await response.Content.ReadFromJsonAsync<LyricAnalysisResponse>();
            _logger.LogInformation("Lyrics analysis completed successfully");
            if(result == null)
            {
                return StatusCode(500, "Invalid AI Response");
            }
            var entity = new AnalysisResult
            {
                AnalysisType = "Lyrics",
                InputText = request.Lyrics,
                Language = result.Language,
                Sentiment = result.Sentiment,
                Mood = result.Mood,
                ResultJson = JsonSerializer.Serialize(result),
                CreatedAt = DateTime.UtcNow,

            };
            _db.AnalysisResults.Add(entity);
            await _db.SaveChangesAsync();
            return Ok(result);
        }
        catch(Exception ex)
        {
            _logger.LogCritical(ex, "Failed to connect to Flask AI service");
            return StatusCode(503,"AI service unavailable. Please try again later.");
        }
    }

    [HttpPost("audio")]
    public async Task<IActionResult> AnalyzeAudio(IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest("No audio file uploaded");

            var flaskUrl = "http://localhost:5001/analyze/audio";

            using var content = new MultipartFormDataContent();
            using var streamContent = new StreamContent(file.OpenReadStream());

            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(streamContent, "file", file.FileName);

            var response = await _http.PostAsync(flaskUrl, content);
            var result = await response.Content.ReadAsStringAsync();
            var audioResult = JsonSerializer.Deserialize<AudioAnalysisResponse>(result);
            if (audioResult == null)
            {
                return StatusCode(500, "Invalid AI Response");
            }
            var entity = new AnalysisResult
            {
                AnalysisType = "Audio",
                FileName = file.FileName,
                ResultJson = result,
                CreatedAt = DateTime.UtcNow,
                MusicalKey = audioResult.Key,
                TempoBpm = audioResult.Tempo_Bpm,
                SampleRate = audioResult.Sample_Rate
            };
            _db.AnalysisResults.Add(entity);
            await _db.SaveChangesAsync();
            return Ok(audioResult);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Failed to connect to Flask AI service");
            return StatusCode(503, "AI service unavailable. Please try again later.");
        }
    }
}

