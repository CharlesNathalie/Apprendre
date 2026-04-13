namespace Apprendre.Models
{
    public class SonFrancais
    {
        [JsonPropertyName("épellation")]
        public List<string> Epellation { get; set; } = new List<string>();

        [JsonPropertyName("exemples")]
        public List<SonFrancaisExemple> Exemples { get; set; } = new List<SonFrancaisExemple>();
    }

    public class SonFrancaisExemple
    {
        [JsonPropertyName("fr")]
        public string Fr { get; set; } = string.Empty;

        [JsonPropertyName("en")]
        public string En { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }
}
