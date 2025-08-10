using MVC_Pratice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Pratice.Controllers
{
    public class EnrollmentController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Enrollment
        public ActionResult Enrollment()
        {
            var model = new Mark
            {
                current_date = DateTime.Now,
            };
            ViewBag.Enrollment = db.Enrollment.
                Include(m => m.Student).
                Include(m => m.Course).
                ToList();
            ViewBag.StudentList = db.Student.ToList();
            ViewBag.CourseList = db.Course.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Enrollment enrollment)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (ModelState.IsValid)
            {
                if (enrollment.Id > 0)
                {
                    var existingEnrollment = db.Mark.Find(enrollment.Id);
                    if (existingEnrollment != null)
                    {
                        existingEnrollment.current_date = enrollment.current_date;
                        existingEnrollment.studentId = enrollment.studentId;
                        existingEnrollment.courseId = enrollment.courseId;
                        db.Entry(existingEnrollment).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["ToastrMessage"] = "Enrollment updated successfully!";
                        TempData["ToastrType"] = "success";
                    }
                }
                else
                {
                    enrollment.current_date = DateTime.Now;
                    db.Enrollment.Add(enrollment);
                    db.SaveChanges();
                    TempData["ToastrMessage"] = "Enrollment Saved successfully!";
                    TempData["ToastrType"] = "success";
                }
                return RedirectToAction("Enrollment");
            }
            return View(enrollment);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var enrollment = db.Enrollment.Find(id);
            if (enrollment != null)
            {
                db.Enrollment.Remove(enrollment);
                db.SaveChanges();
                TempData["ToastrMessage"] = "Enrollment Deleted successfully!";
                TempData["ToastrType"] = "success";
            }
            return RedirectToAction("Enrollment");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var enrollment = db.Enrollment.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Enrollment = db.Enrollment.ToList();
            ViewBag.StudentList = db.Student.ToList();
            ViewBag.CourseList = db.Course.ToList();
            return View("Enrollment", enrollment);
        }
    }
}