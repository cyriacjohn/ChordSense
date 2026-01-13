namespace ChordSense.Api.Models.Responses
{
    public class LyricAnalysisResponse
    {
        public string WesternKey { get; set; }
        public string[] WesternChords { get; set; }
        public string CarnaticRaga { get; set; }
        public string HindustanRaga { get; set; }
        public string[] Swaras { get; set; }
    }
}
