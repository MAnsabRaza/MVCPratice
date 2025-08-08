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
            ViewBag.Student = db.Student.ToList();
            return View(new Student()); 
        }
        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                if (student.Id > 0) 
                {
                    var existingStudent = db.Student.Find(student.Id);
                    if (existingStudent != null)
                    {
                        existingStudent.student_name = student.student_name;
                        existingStudent.age = student.age;
                        existingStudent.batch = student.batch;
                        db.Entry(existingStudent).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                else
                {
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