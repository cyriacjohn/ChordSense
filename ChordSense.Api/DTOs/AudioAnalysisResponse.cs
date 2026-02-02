using System.Text.Json.Serialization;

namespace ChordSense.Api.DTOs
{
    public class AudioAnalysisResponse
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }
        [JsonPropertyName("tempo_bpm")]
        public double Tempo_Bpm { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }
        [JsonPropertyName("sample_rate")]
        public int Sample_Rate { get; set; }
    }
}
