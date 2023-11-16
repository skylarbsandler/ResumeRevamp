using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ResumeRevamp.DataAccess;
using ResumeRevamp.Models;
using Serilog;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Composition;

namespace ResumeRevamp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ResumeRevampContext _context;

        public UsersController(ResumeRevampContext context)
        {
            _context = context;
        }

        [Route("/users/login")]
        public IActionResult Login()
        {

            return View();

        }

        [HttpPost]
        [Route("/users/login")]
        public IActionResult CheckPassword(string password, string username)
        {

            if (password == null || username == null)
            {
                ModelState.AddModelError("LoginFail", "Wrong password or username. Try again!");

                return View("Login");

            }

            var user = _context.Users
                .Where(user => user.Username == username
                && user.Password == EncodePassword(password))
                .FirstOrDefault();


            if (user == null)
            {
                ModelState.AddModelError("LoginFail", "Wrong password or username. Try again!");

                return View("Login");

            }

            else
            {
                Response.Cookies.Append("CurrentUser", user.Id.ToString());

                return Redirect($"/users/profile");

            }
        }


        [Route("/users/logout")]
        public IActionResult Logout()
        {

            Response.Cookies.Delete("CurrentUser");

            return Redirect("/");

        }


        [Route("/Users/Signup")]
        public IActionResult Signup()
        {
            ViewData["UserCreateError"] = TempData["UserCreateError"];
            return View();
        }


        [HttpPost]
        [Route("/Users/")]
        public IActionResult Create(User user)
        {
            if (!ModelState.IsValid)
            {
                Log.Warning("User model not valid for create action");
                TempData["UserCreateError"] = "One or more fields were invalid, please try again";

                return View("Signup", user);
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Username == user.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username is already taken. Please choose a different one.");
                return View("Signup", user);
            }

            User userModel = new User();

            string digestedPassword = userModel.GetDigestedPassword(user.Password);

            user.Password = digestedPassword;

            _context.Add(user);

            _context.SaveChanges();
            Log.Information("A user has been created");

            Response.Cookies.Append("CurrentUser", user.Id.ToString());
            return RedirectToAction("profile", new { userId = user.Id });
        }


        [Route("/Users/Profile")]
        public IActionResult Profile(bool passwordChanged = false, bool userUpdated = false)
        {
            if (!Request.Cookies.ContainsKey("CurrentUser"))
            {
                return Redirect("/");
            }
            string id = Request.Cookies["CurrentUser"].ToString();

            if (int.TryParse(id, out int parsedId))
            {
                var user1 = _context.Users
              .Where(u => u.Id == parsedId)
              .FirstOrDefault();

                ViewBag.PasswordChanged = passwordChanged;
                ViewBag.UserUpdated = userUpdated;


                return View(user1);
            }
            else
            {
                Response.Cookies.Delete("CurrentUser");
                return NotFound();
            }
        }

        [Route("/Users/{id:int}/Edit")]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            if (!IsCurrentUser((int)id))
            {
                return BadRequest();
            }

            var user = _context.Users.Find(id);

            if (user is null)
            {
                return NotFound();
            }

            return View(user);

        }

        [HttpPost]
        [Route("/Users/update/{userId:int}")]
        public IActionResult Update(int? userId, User user)
        {
            if (userId is null)
            {
                return NotFound();
            }

            var existingUser = _context.Users.Find(userId);

            existingUser.Name = user.Name;

            existingUser.Username = user.Username;

            existingUser.Email = user.Email;

            _context.SaveChanges();
            Log.Information("A user's information has been updated.");

            return RedirectToAction("profile", new { userId = user.Id, userUpdated = true });

        }

        [Route("/users/delete/{userId:int}")]
        public IActionResult Delete(int? userId)
        {
            if (userId is null)
            {
                return NotFound();
            }

            if (IsCurrentUser((int)userId))
            {
                var user = _context.Users.Find(userId);

                _context.Users.Remove(user);

                _context.SaveChanges();

                Response.Cookies.Delete("CurrentUser");
                Log.Information($"A user has been deleted, id: {userId}");

                return Redirect("/");
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("/Users/{id:int}/ResetPassword")]
        public IActionResult ResetPassword(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }
            if (Request.Cookies.ContainsKey("CurrentUser"))
            {
                var currentUserId = Request.Cookies["CurrentUser"];

                if (currentUserId != id.ToString())
                {
                    return StatusCode(403);
                }

                var user = _context.Users.Find(id);

                return View(user);
            }
            else
            {
                return Redirect("/users/login");
            }
        }

        [Route("/Users/favorites")]
        public IActionResult Favorites()
        {
            if (!Request.Cookies.ContainsKey("CurrentUser"))
            {
                return Redirect("/");
            }

            string id = Request.Cookies["CurrentUser"].ToString();

            if (int.TryParse(id, out int parsedId))
            {
                var user1 = _context.Users
              .Include(u => u.Favorites)
              .Where(u => u.Id == parsedId)
              .FirstOrDefault();

                return View(user1);
            }
            else
            {
                Response.Cookies.Delete("CurrentUser");
                return NotFound();
            }

            return View();
        }


        [Route("/Users/updatepassword/{id}")]
        public IActionResult UpdatePassword(int? id, string newPassword)
        {
            if (id is null)
            {
                return NotFound();
            }
            var user = _context.Users.Find(id);
            if (user is null)
            {
                return NotFound();
            }

            string digestedPassword = EncodePassword(newPassword);

            user.Password = digestedPassword;

            _context.SaveChanges();
            Log.Information("A user's password has been changed.");

            return RedirectToAction("profile", new { userId = user.Id, passwordChanged = true });

        }

        private string EncodePassword(string password)
        {
            HashAlgorithm sha = SHA256.Create();

            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);

            byte[] passwordDigested = sha.ComputeHash(passwordBytes);

            StringBuilder passwordBuilder = new StringBuilder();

            foreach (byte b in passwordDigested)
            {
                passwordBuilder.Append(b.ToString("x2"));
            }

            return passwordBuilder.ToString();

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

