namespace ResumeRevamp.Models
{
    public class Favorite
    {
        public int Id { get; set; }
        public Word Word { get; set; }
        public User User { get; set; }
    }
}
