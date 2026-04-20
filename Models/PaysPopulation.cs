namespace Apprendre.Models
{
    public partial class PaysPopulation
    {
        [JsonPropertyName("countryNameFr")]
        public string CountryNameFr { get; set; } = string.Empty;

        [JsonPropertyName("countryNameEn")]
        public string CountryNameEn { get; set; } = string.Empty;

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("capitalFr")]
        public string CapitalFr { get; set; } = string.Empty;

        [JsonPropertyName("capitalEn")]
        public string CapitalEn { get; set; } = string.Empty;
    }
}
