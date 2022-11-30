using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SeniorProject.Models;
using System.Diagnostics;
using SeniorProject.Models.Context;
using System.Security.Cryptography;

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

            user.password = BCrypt.Net.BCrypt.HashPassword(user.password);

            if (ModelState.IsValid)
            {
                bool userExists = _context.User.Any(x => x.username == user.username);
                bool mobileExists = _context.User.Any(y => y.mobile == user.mobile);
                if (userExists)
                {
                    ViewBag.Message = "This Username Already Exists.";
                }
                else if (mobileExists)
                {
                    ViewBag.Message = "The Given Phone Number is Already in Use.";
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
            var usr = _context.User.SingleOrDefault(x => x.username == user.username);
            bool isValidPassword = BCrypt.Net.BCrypt.Verify(user.password, usr.password);
            if (isValidPassword)
            {
                HttpContext.Session.SetString("userID", usr.userID.ToString());
                HttpContext.Session.SetString("username", usr.username);
                HttpContext.Session.SetString("isAdmin", usr.isAdmin.ToString());
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
                ViewBag.userID = HttpContext.Session.GetString("userID");
                ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
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
                ViewBag.userID = HttpContext.Session.GetString("userID");
                //ViewBag.userID = userID;
                return View(_context.User.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("lists")]
        public IActionResult Lists()
        {
            if (HttpContext.Session.GetString("userID") != null)
            {
                ViewBag.Username = HttpContext.Session.GetString("username");
                ViewBag.userID = HttpContext.Session.GetString("userID");
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
                ViewBag.userID = HttpContext.Session.GetString("userID");
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
                ViewBag.userID = HttpContext.Session.GetString("userID");
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
                ViewBag.userID = HttpContext.Session.GetString("userID");
                return View(_context.User.ToList());
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [Route("admin")]
        public IActionResult Admin()
        {
            if (HttpContext.Session.GetString("isAdmin") == "True")
            {
                ViewBag.Username = HttpContext.Session.GetString("username");
                ViewBag.userID = HttpContext.Session.GetString("userID");
                ViewBag.isAdmin = HttpContext.Session.GetString("isAdmin");
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