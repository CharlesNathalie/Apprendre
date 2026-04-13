namespace Apprendre.Models
{
    public class SoundEnglish
    {
        [JsonPropertyName("spelling")]
        public List<string> Spelling { get; set; } = new List<string>();

        [JsonPropertyName("examples")]
        public List<SoundEnglishExample> Examples { get; set; } = new List<SoundEnglishExample>();
    }

    public class SoundEnglishExample
    {
        [JsonPropertyName("en")]
        public string En { get; set; } = string.Empty;

        [JsonPropertyName("fr")]
        public string Fr { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
