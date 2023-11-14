using Microsoft.AspNetCore.Mvc;

namespace ResumeRevamp.Controllers
{
    public class WordsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
