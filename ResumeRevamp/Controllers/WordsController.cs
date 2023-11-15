using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NuGet.Packaging.Signing;
using ResumeRevamp.DataAccess;
using ResumeRevamp.Interfaces;
using ResumeRevamp.Models;
using Serilog;
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
                var user = _context.Users.Include(u => u.Favorites).FirstOrDefault(u => u.Id == parsedId);
                var word = _context.Words.FirstOrDefault(w => w.OriginalWord == originalWord);

                if (user != null && word != null)
                {
                    if (!user.Favorites.Contains(word))
                    {
                        user.Favorites.Add(word);
                        user.FavoritesCount++; 

                        //_context.User.Update(user);
                        _context.SaveChanges();
                    }

                    return RedirectToAction("Favorites", "Users");
                }
                else
                {
                    // Handle the case where the user or word was not found
                    return NotFound();
                }
            }
            else
            {
                // Handle the case where parsing the id fails
                return NotFound();
            }
        }

        [HttpPost]
        [Route("/Words/RemoveFavoriteWord")]
        public IActionResult RemoveFavoriteWord(string originalWord, string synonyms)
        {
            string id = Request.Cookies["CurrentUser"].ToString();

            if (int.TryParse(id, out int parsedId))
            {
                var user = _context.Users.Include(u => u.Favorites).FirstOrDefault(u => u.Id == parsedId);
                var word = _context.Words.FirstOrDefault(w => w.OriginalWord == originalWord);

                if (user != null && word != null)
                {
                    if (user.Favorites.Contains(word))
                    {
                        user.Favorites.Remove(word);
                        user.FavoritesCount--;

                        //_context.User.Update(user);
                        _context.SaveChanges();

                        Log.Information($"A word has been removed from Favorites by user: [{user.Id}]{user.Username}");
                    }

                    return RedirectToAction("Favorites", "Users");
                }
                else
                {
                    // Handle the case where the user or word was not found
                    return NotFound();
                }
            }
            else
            {
                // Handle the case where parsing the id fails
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
