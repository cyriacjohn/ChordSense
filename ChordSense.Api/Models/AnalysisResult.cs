namespace ChordSense.Api.Models
{
    public class AnalysisResult
    {
        public int Id { get; set;  }

        //Metadata
        public string AnalysisType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        //Lyrics
        public string? InputText {  get; set; }
        public string? Sentiment { get; set; }
        public string? Language { get; set; }
        public string? Mood { get; set; }
        public string ResultJson { get; set; } = string.Empty;

        //Audio
        public string? FileName { get; set; }
        public string? MusicalKey { get; set; }
        public double? TempoBpm { get; set; }
        public int? SampleRate { get; set; }


    }
}
