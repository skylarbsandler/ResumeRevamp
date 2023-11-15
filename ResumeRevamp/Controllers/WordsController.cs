using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;
using ResumeRevamp.DataAccess;
using ResumeRevamp.Interfaces;
using ResumeRevamp.Models;
using System.Diagnostics;

namespace ResumeRevamp.Controllers
{
    public class WordsController : Controller
    {
        private readonly IWordsApiService _wordsApiService;
        private readonly ILogger<HomeController> _logger;
        private readonly ResumeRevampContext _context;

        public WordsController(IWordsApiService wordsApiService, ILogger<HomeController> logger, ResumeRevampContext context)
        {
            _logger = logger;
            _wordsApiService = wordsApiService;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/Words/AddFavoriteWord")]
        public IActionResult AddFavoriteWord(string originalWord, string synonyms)
        {
            string id = Request.Cookies["CurrentUser"].ToString();

            if (int.TryParse(id, out int parsedId))
            {
                var user = _context.Users.Where(u => u.Id == parsedId).FirstOrDefault(); ;
                var word = _context.Words.Where(w => w.OriginalWord == originalWord).FirstOrDefault(); ;

                user.AddFavorite(word);
                user.FavoritesCount++;
                //_context.Users.Update(user);
                _context.SaveChanges();

                return RedirectToAction("Favorites", "Users");
            }
            else
            {
                return NotFound();
            }
        }

        private bool IsCurrentUser(int userId)
        {
            if (!Request.Cookies.ContainsKey("CurrentUser"))
            {
                return false;
            }
            if (int.TryParse(Request.Cookies["CurrentUser"], out int parseId))
            {
                if (userId == parseId)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
