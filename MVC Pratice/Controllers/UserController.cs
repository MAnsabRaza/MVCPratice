using System.Linq;
using System.Net;
using System.Web.Mvc;
using MVC_Pratice.Models;
using System.Data.Entity;

namespace MVC_Pratice.Controllers
{
    public class UserController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            ViewBag.User = db.User.ToList();
            return View(new User()); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Id > 0) 
                {
                    var existing = db.User.Find(user.Id);
                    if (existing != null)
                    {
                        if (db.User.Any(u => u.email == user.email && u.Id != user.Id))
                        {
                            ViewBag.Error = "Email is already registered. Please use another email.";
                            ViewBag.User = db.User.ToList();
                            return View("Index", user);
                        }
                        existing.name = user.name;
                        existing.phone_number = user.phone_number;
                        existing.address = user.address;
                        existing.email = user.email;
                        if (!string.IsNullOrWhiteSpace(user.password))
                        {
                            existing.password = BCrypt.Net.BCrypt.HashPassword(user.password);
                        }

                        db.Entry(existing).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    var existingEmail = db.User.FirstOrDefault(u => u.email == user.email);
                    if (db.User.Any(u => u.email == user.email))
                    {
                        ViewBag.Error = "Email is already registered. Please use another email.";
                        ViewBag.User = db.User.ToList();
                        return View("Index", user);
                    }
                    user.password=BCrypt.Net.BCrypt.HashPassword(user.password);
                        db.User.Add(user);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.User = db.User.ToList();
            return View("Index", user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var user = db.User.Find(id);
            if (user != null)
            {
                db.User.Remove(user);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = db.User.Find(id);
            if(user == null)
            {
                return HttpNotFound();
            }
            ViewBag.User = db.User.ToList();
            return View("Index", user);
        }
    }
}
