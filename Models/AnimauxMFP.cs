namespace Apprendre.Models
{
    public partial class AnimauxMFP
    {
        [JsonPropertyName("mâle")]
        public string Male { get; set; } = string.Empty;

        [JsonPropertyName("femelle")]
        public string Female { get; set; } = string.Empty;

        [JsonPropertyName("petit")]
        public string Petit { get; set; } = string.Empty;

        [JsonPropertyName("en")]
        public string En { get; set; } = string.Empty;

        [JsonPropertyName("young")]
        public string Young { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
