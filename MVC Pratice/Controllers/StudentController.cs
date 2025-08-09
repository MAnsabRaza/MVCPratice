using MVC_Pratice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Pratice.Controllers
{
    public class StudentController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            var model = new Student
            {
                current_date=DateTime.Now,
            };
            ViewBag.Student = db.Student.ToList();
            return View(model); 
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (ModelState.IsValid)
            {
                if (student.Id > 0) 
                {
                    var existingStudent = db.Student.Find(student.Id);
                    if (existingStudent != null)
                    {
                        existingStudent.student_name = student.student_name;
                        existingStudent.age = student.age;
                        existingStudent.address = student.address;
                        existingStudent.userId = Convert.ToInt32(Session["userId"]);
                        db.Entry(existingStudent).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
                    student.userId=Convert.ToInt32(Session["userId"]);
                    student.current_date = DateTime.Now;
                    db.Student.Add(student);
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var student = db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            db.Student.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student=db.Student.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.Student = db.Student.ToList();
            return View("Index",student);
        }

    }
}