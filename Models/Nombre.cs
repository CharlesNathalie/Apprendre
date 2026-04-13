namespace Apprendre.Models
{
    public partial class Nombre
    {
        [JsonPropertyName("nombre")]
        public string NombreValue { get; set; } = string.Empty;

        [JsonPropertyName("fr")]
        public string Fr { get; set; } = string.Empty;

        [JsonPropertyName("en")]
        public string En { get; set; } = string.Empty;
    }
}
