namespace Apprendre.Models
{
    public partial class FrEnURL
    {
        [JsonPropertyName("fr")]
        public string Fr { get; set; } = string.Empty;

        [JsonPropertyName("en")]
        public string En { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
