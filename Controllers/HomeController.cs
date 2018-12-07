using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using LogReg.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;


namespace LogReg.Controllers
{
    public class HomeController : Controller
    {
        private LogRegContext dbContext;
        public HomeController(LogRegContext context)
        {
            dbContext = context;
        }
        

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Hacker")]
        public IActionResult Hacker()
        {
            return View();
        }


        [HttpGet]
        [Route("/success")]
        public IActionResult Success()
        {
            if(HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Hacker");
            }
            return View();
        }


        [HttpPost("RegisterUser")]
        public IActionResult RegisterUser(User user)
        {
            if(ModelState.IsValid)
            {
                if(dbContext.User.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already in use!");
                    return View();
                }
                else
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    user.Password = Hasher.HashPassword(user, user.Password);

                    dbContext.Add(user);
                    dbContext.SaveChanges();
                    var AddedUser =  dbContext.User.FirstOrDefault(u => u.Email == user.Email);
                    HttpContext.Session.SetInt32("UserId", AddedUser.Id);
                    int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");

                    return RedirectToAction("Success");
                }        
            }
            else
            {
                return View();
            }
        }

        [HttpGet("ViewLoginUser")]
        public IActionResult ViewLoginUser()
        {
            return View();
        }

        [HttpPost("LoginUser")]
        public IActionResult LoginUser(LoginUser UserSubmission)
        {
            if(ModelState.IsValid)
            {
                var UserToCheck = dbContext.User.FirstOrDefault(u => u.Email == UserSubmission.Email);
                if(UserToCheck.Email == null)
                {
                    ModelState.AddModelError("Email", "Invalid Email or Password");
                    return View("ViewLoginUser");
                }
            var hasher = new PasswordHasher<LoginUser>();

            var result = hasher.VerifyHashedPassword(UserSubmission, UserToCheck.Password, UserSubmission.Password);

            if (result == 0)
            {
                ModelState.AddModelError("Password", "Invalid Email or Password");
                return View("ViewLoginUser");
            }
                HttpContext.Session.SetInt32("UserId", UserToCheck.Id);
                int? LoggedInUserId = HttpContext.Session.GetInt32("UserId");
                return RedirectToAction("Success");
            }
            return View("ViewLoginUser");
        }
    }
}
