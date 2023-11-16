using System.Text.Json.Serialization;

namespace ResumeRevamp.Models
{
    public class Definition
    {
        public int Id { get; set; }
        [JsonPropertyName("definition")]
        public string? DefinitionText { get; set; }
        [JsonPropertyName("partOfSpeech")]
        public string? PartOfSpeech { get; set; }
    }
}
