namespace ChordSense.Api.Models.Responses
{
    public class LyricAnalysisResponse
    {
        public string Status { get; set; }
        public string Language { get; set; }
        public string Sentiment { get; set; }
        public string Mood { get; set; }
    }
}
