namespace Apprendre.Models
{
    public partial class ColorName
    {
        [JsonPropertyName("colorname")]
        public string ColorNameValue { get; set; } = string.Empty;

        [JsonPropertyName("fr")]
        public string Fr { get; set; } = string.Empty;

        [JsonPropertyName("en")]
        public string En { get; set; } = string.Empty;
    }
}
