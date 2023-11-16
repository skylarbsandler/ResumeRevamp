using ResumeRevamp.Models;

namespace ResumeRevamp.Interfaces
{
    public interface IWordsApiService
    {
        Task<List<string>> GetSynonymsAsync(Word word);
        Task<List<Definition>> GetDefinitionAsync(Word word);
    }
}
