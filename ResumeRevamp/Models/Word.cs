namespace ResumeRevamp.Models
{
    public class Word
    {
        public int Id { get; set; }
        public string OriginalWord { get; set; }
        public List<string> Synonyms { get; set; }
    }
}
