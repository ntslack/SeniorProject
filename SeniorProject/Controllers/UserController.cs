//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using SeniorProject.Models;
//using System.Diagnostics;
//using SeniorProject.Models.Context;

//namespace SeniorProject.Controllers
//{
//    [Route("api/user")]
//    [ApiController]
//    public class UserController : Controller
//    {
//        private readonly ILogger<UserController> _logger;
//        private DatabaseContext _context;

//        public UserController(ILogger<UserController> logger, DatabaseContext context)
//        {
//            _logger = logger;
//            _context = context;
//        }

//        [HttpPost("register")]
//        public ActionResult Register(UserAccount user)
//        {
//            if (ModelState.IsValid)
//            {
//                bool userExists = _context.User.Any(x => x.username == user.username);
//                bool mobileExists = _context.User.Any(y => y.mobile == user.mobile);
//                bool emailExists = _context.User.Any(z => z.email == user.email);
//                if (userExists)
//                {
//                    ViewBag.Message = "This Username Already Exists.";
//                }
//                else if (mobileExists)
//                {
//                    ViewBag.Message = "The Given Phone Number is Already in Use.";
//                }
//                else if (emailExists)
//                {
//                    ViewBag.Message = "The Given Email is Already in Use.";
//                }
//                else
//                {
//                    _context.User.Add(user);
//                    _context.SaveChanges();

//                    ModelState.Clear();
//                    ViewBag.Message = user.firstName + " " + user.lastName + " has been registered";
//                }
//            }
//            return View();
//        }

//        [HttpPost("register")]
//        public ActionResult Login(UserAccount user)
//        {
//            var account = _context.User.Where(u => u.username == user.username && u.password == user.password).FirstOrDefault();
//            if (account != null)
//            {
//                HttpContext.Session.SetString("userID", account.userID.ToString());
//                HttpContext.Session.SetString("username", account.username);
//                return RedirectToAction("Home");
//            }
//            else
//            {
//                ModelState.AddModelError("", "Username or Password is Incorrect");
//            }
//            return View();
//        }
//    }
//}
