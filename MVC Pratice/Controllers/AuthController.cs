using MVC_Pratice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Pratice.Controllers
{
    public class AuthController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Auth
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var existingEmail=db.User.FirstOrDefault(u=>u.email ==  user.email);
                if(existingEmail!=null)
                {
                    ViewBag.Error = "Email is already registered. Please use another email.";
                    return View(user);
                }
                user.password = BCrypt.Net.BCrypt.HashPassword(user.password);
                db.User.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            var user = db.User.FirstOrDefault(u => u.email == login.email && u.password == login.password);
            if (user != null)
            {
                Session["userId"] = user.Id;
                Session["userEmail"] = user.email;
                return RedirectToAction("Index", "Student");
            }

            ViewBag.Error = "Invalid email or password";
            return View(login);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}