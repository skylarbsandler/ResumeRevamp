using System.Text.Json.Serialization;

namespace ResumeRevamp.Models
{
    public class DefinitionResponse
    {
        public string Word { get; set; }
        public List<Definition> Definitions { get; set; }
    }
}
