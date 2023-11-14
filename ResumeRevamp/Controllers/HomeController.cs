using Microsoft.AspNetCore.Mvc;
using ResumeRevamp.DataAccess;
using ResumeRevamp.Interfaces;
using ResumeRevamp.Models;
using System.Diagnostics;

namespace ResumeRevamp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWordsApiService _wordsApiService;
        private readonly ILogger<HomeController> _logger;
        private readonly ResumeRevampContext _context;

        public HomeController(IWordsApiService wordsApiService, ILogger<HomeController> logger, ResumeRevampContext context)
        {
            _logger = logger;
            _wordsApiService = wordsApiService;
            _context = context;
        }

        public async Task<IActionResult> Index(Word word)
        {
            List<string> synonyms = new List<string>();

            if (word.OriginalWord != null)
            {
                synonyms = await _wordsApiService.GetSynonymsAsync(word);
            }
            //else
            //{
            //    return BadRequest(new { error = "OriginalWord cannot be null." });
            //}

            var wordWithSynonyms = new Word { OriginalWord = word.OriginalWord, Synonyms = synonyms };

            //if (wordWithSynonyms.OriginalWord != null)
            //{
            //    _context.Words.Add(wordWithSynonyms);
            //    _context.SaveChanges();
            //}
            //else
            //{
            //    return BadRequest(new { error = "wordWithSynonyms cannot be null." });
            //}

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