using ResumeRevamp.Interfaces;
using ResumeRevamp.Models;
using System.Text.Json;
using System.Net.Http;
using System.Numerics;
using System.Net.Http.Headers;
using Newtonsoft;

namespace ResumeRevamp.Services
{
    public class WordsApiService : IWordsApiService
    {
        private static readonly HttpClient client;

        static WordsApiService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://wordsapiv1.p.rapidapi.com/")
            };
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "wordsapiv1.p.rapidapi.com");
        }

        public async Task<List<string>> GetSynonymsAsync(Word word)
        {
            try
            {
                if (word.OriginalWord != null)
                {
                    string url = $"words/{word.OriginalWord}/synonyms";
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var stringResponse = await response.Content.ReadAsStringAsync();
                        SynonymsResponse result = JsonSerializer.Deserialize<SynonymsResponse>(stringResponse, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                        return result.Synonyms;
                    }
                    else
                    {
                        //Add logging
                        return new List<string>();
                    }
                }
                else
                {
                    // Handle the case where OriginalWord is null or empty.
                    // For now, returning an empty list.
                    return new List<string>();
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception appropriately.
                throw;
            }
        }

        public async Task<List<Definition>> GetDefinitionAsync(Word word)
        {
            try
            {
                if (word.OriginalWord != null)
                {
                    string url = $"words/{word.OriginalWord}/definitions";
                    var response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        var stringResponse = await response.Content.ReadAsStringAsync();
                        DefinitionResponse result = JsonSerializer.Deserialize<DefinitionResponse>(stringResponse, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                        return result.Definitions;
                    }
                    else
                    {
                        //Add logging
                        return new List<Definition>();
                    }
                }
                else
                {
                    // Handle the case where OriginalWord is null or empty.
                    // For now, returning an empty list.
                    return new List<Definition>();
                }
            }
            catch (HttpRequestException ex)
            {
                // Log or handle the exception appropriately.
                throw;
            }
        }
    }
}
