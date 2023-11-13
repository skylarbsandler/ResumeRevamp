using Microsoft.AspNetCore.Mvc;
using ResumeRevamp.Interfaces;
using ResumeRevamp.Models;
using System.Diagnostics;

namespace ResumeRevamp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordsApiService _wordsApiService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IWordsApiService wordsApiService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _wordsApiService = wordsApiService;
        }

        public async Task<IActionResult> Index(Word word)
        {
            List<string> synonyms = new List<string>();

            if (word.OriginalWord != null)
            {
                synonyms = await _wordsApiService.GetSynonymsAsync(word);
            }

            var wordWithSynonyms = new Word { OriginalWord = word.OriginalWord, Synonyms = synonyms };
            return View(wordWithSynonyms);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}