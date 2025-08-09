using MVC_Pratice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Pratice.Controllers
{
    public class CourseController : Controller
    {
        private AppDbContext db = new AppDbContext();
        public ActionResult Course()
        {
            ViewBag.Course = db.Course.ToList();
            return View(new Course());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Course course)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (ModelState.IsValid)
            {
                if (course.Id > 0)
                {
                    var existingCourse = db.Course.Find(course.Id);
                    if (existingCourse != null)
                    {
                        existingCourse.current_date = course.current_date;
                        existingCourse.course_name = course.course_name;
                        existingCourse.userId = Convert.ToInt32(Session["userId"]);
                        db.Entry(existingCourse).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    course.userId = Convert.ToInt32(Session["userId"]);
                    course.current_date = DateTime.Now;
                    db.Course.Add(course);
                    db.SaveChanges();
                }
                return RedirectToAction("Course");
            }
            ViewBag.Course=db.Course.ToList();
            return View("Course",course);

        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var course=db.Course.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            db.Course.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Course");

        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var course=db.Course.Find(id);
            if(course == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course = db.Course.ToList();
            return View("Course", course);
        }

    }
}