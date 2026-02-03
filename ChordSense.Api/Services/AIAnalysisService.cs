using System.Net.Http.Headers;
using System.Text.Json;
using System.Net.Http.Json;
using ChordSense.Api.DTOs;
using ChordSense.Api.Services.Interfaces;
using ChordSense.Api.Models.Responses;

namespace ChordSense.Api.Services
{
    public class AIAnalysisService : IAIAnalysisService
    {
        private readonly HttpClient _http;
        public AIAnalysisService(HttpClient http)
        {
            _http = http;
        }

        public async Task<LyricAnalysisResponse> AnalyseLyricsAsync(string lyrics)
        {
            var response = await _http.PostAsJsonAsync("http://localhost:5001/analyze/lyrics", new { lyrics });
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<LyricAnalysisResponse>();
        }

        public async Task<AudioAnalysisResponse> AnalyseAudioAsync(IFormFile audio)
        {
            using var content = new MultipartFormDataContent();
            using var stream = new StreamContent(audio.OpenReadStream());
            stream.Headers.ContentType =new MediaTypeHeaderValue(audio.ContentType);
            content.Add(stream, "file", audio.FileName);
            var response = await _http.PostAsync("http://localhost:5001/analyze/audio", content);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AudioAnalysisResponse>(json);
        }
    }
}
