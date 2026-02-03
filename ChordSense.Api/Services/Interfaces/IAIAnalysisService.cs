using ChordSense.Api.DTOs;
using ChordSense.Api.Models.Requests;
using Microsoft.AspNetCore.Http;
namespace ChordSense.Api.Services.Interfaces;
using ChordSense.Api.Models.Responses;

public interface IAIAnalysisService
{
    Task<LyricAnalysisResponse> AnalyseLyricsAsync(string lyrics);
    Task<AudioAnalysisResponse> AnalyseAudioAsync(IFormFile audio);
}
