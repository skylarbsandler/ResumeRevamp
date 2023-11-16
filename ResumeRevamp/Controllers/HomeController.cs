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

        public IActionResult Index()
        {

            if (Request.Cookies.ContainsKey("CurrentUser"))
            {
                return Redirect("/users/profile");
            }

            else
            {
                return View();
            }

        }

        public IActionResult RevampForm()
        {
            return View();
        }

        //public async Task<IActionResult> Definition(Word word)
        //{
        //    List<Definition> definitions = new List<Definition>();

        //    if (word.OriginalWord != null)
        //    {
        //        definitions = await _wordsApiService.GetDefinitionAsync(word);
        //    }

        //    var wordWithDefinitions = new Word { OriginalWord = word.OriginalWord, Definitions = definitions };
        //    _context.Words.Add(wordWithDefinitions);
        //    _context.SaveChanges();

        //    return View(wordWithDefinitions);
        //}

        //public async Task<IActionResult> Revamp(Word word)
        //{
        //    List<string> synonyms = new List<string>();

        //    if (word.OriginalWord != null)
        //    {
        //        synonyms = await _wordsApiService.GetSynonymsAsync(word);
        //    }

        //    var wordWithSynonyms = new Word { OriginalWord = word.OriginalWord, Synonyms = synonyms };

        //    _context.Words.Add(wordWithSynonyms);
        //    _context.SaveChanges();

        //    return View(wordWithSynonyms);
        //}

        public async Task<IActionResult> Revamp(Word word)
        {
            List<Definition> definitions = new List<Definition>();
            List<string> synonyms = new List<string>();

            if (word.OriginalWord != null)
            {
                definitions = await _wordsApiService.GetDefinitionAsync(word);
                synonyms = await _wordsApiService.GetSynonymsAsync(word);
            }

            var wordWithDefinitionsSynonyms = new Word
            {
                OriginalWord = word.OriginalWord,
                Definitions = definitions,
                Synonyms = synonyms
            };

            _context.Words.Add(wordWithDefinitionsSynonyms);
            _context.SaveChanges();

            return View(wordWithDefinitionsSynonyms);
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