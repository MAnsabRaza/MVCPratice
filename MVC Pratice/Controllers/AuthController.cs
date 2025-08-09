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
                TempData["ToastrMessage"] = "User Added successfully!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            var user = db.User.FirstOrDefault(u => u.email == login.email);
            if (user != null && BCrypt.Net.BCrypt.Verify(login.password,user.password))
            {
                Session["userId"] = user.Id;
                Session["userEmail"] = user.email;
                Session["userName"] = user.name;
                TempData["ToastrMessage"] = "User Login successfully!";
                TempData["ToastrType"] = "success";
                return RedirectToAction("Home", "Home");
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