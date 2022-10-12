using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using System.Diagnostics;
using SeniorProject.Models.Context;

namespace SeniorProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, DatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.User.ToList());
        }

        [Route("register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register(UserAccount user)
        {
            if (ModelState.IsValid)
            {
                bool userExists = _context.User.Any(x => x.username == user.username);
                bool mobileExists = _context.User.Any(y => y.mobile == user.mobile);
                bool emailExists = _context.User.Any(z => z.email == user.email);
                if (userExists)
                {
                    ViewBag.Message = "This Username Already Exists.";
                }
                else if (mobileExists)
                {
                    ViewBag.Message = "The Given Phone Number is Already in Use.";
                }
                else if (emailExists)
                {
                    ViewBag.Message = "The Given Email is Already in Use.";
                }
                else
                {
                    _context.User.Add(user);
                    _context.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = user.firstName + " " + user.lastName + " has been registered";
                }
            }
            return View();
        }

        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(UserAccount user)
        {
            var account = _context.User.Where(u => u.username == user.username && u.password == user.password).FirstOrDefault();
            if (account != null)
            {
                HttpContext.Session.SetString("userID", account.userID.ToString());
                HttpContext.Session.SetString("username", account.username);
                return RedirectToAction("Home");
            } else
            {
                ModelState.AddModelError("", "Username or Password is Incorrect");
            }
            return View();
        }

        [Route("home")]
        public IActionResult Home()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("username");
                ViewBag.Id = HttpContext.Session.GetString("userID");
                return View(_context.User.ToList());
            } else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("notes")]
        public IActionResult Notes()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("username");
                return View(_context.User.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("calendar")]
        public IActionResult Calendar()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("username");
                return View(_context.User.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("tasks")]
        public IActionResult Tasks()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("username");
                return View(_context.User.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("budget")]
        public IActionResult Budget()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("username");
                return View(_context.User.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}