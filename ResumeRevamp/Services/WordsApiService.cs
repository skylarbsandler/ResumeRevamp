using ResumeRevamp.Interfaces;
using ResumeRevamp.Models;
using System.Text.Json;
using System.Net.Http;
using System.Numerics;
using System.Net.Http.Headers;

namespace ResumeRevamp.Services
{
    public class WordsApiService : IWordsApiService
    {
        private static readonly HttpClient client;

        static WordsApiService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://wordsapiv1.p.rapidapi.com/");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "wordsapiv1.p.rapidapi.com");
        }

        public async Task<List<string>> GetSynonymsAsync(Word word)
        {
            try
            {
                var url = string.Format($"words/{word}/synonyms");
                var response = await client.GetAsync(url);
                var stringResponse = await response.Content.ReadAsStringAsync();
                SynonymsResponse result = JsonSerializer.Deserialize<SynonymsResponse>(stringResponse);
                return result.Synonyms;
            }
            catch (HttpRequestException ex)
            {
                throw;
            }
        }
    }
}
