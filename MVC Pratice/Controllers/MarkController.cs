using MVC_Pratice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Pratice.Controllers
{
    public class MarkController : Controller
    {
        private AppDbContext db=new AppDbContext();
        // GET: Mark
        public ActionResult Mark()
        {
            var model = new Mark
            {
                current_date = DateTime.Now,
            };
            ViewBag.Mark = db.Mark.
                Include(m=>m.User).
                Include(m=>m.Student).
                Include(m=>m.Course).
                ToList();
            ViewBag.StudentList = db.Student.ToList();
            ViewBag.CourseList = db.Course.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Mark mark)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (ModelState.IsValid)
            {
                if(mark.Id > 0)
                {
                    var existingMark = db.Mark.Find(mark.Id);
                    if(existingMark != null)
                    {
                        existingMark.current_date = mark.current_date;
                        existingMark.studentId = mark.studentId;
                        existingMark.courseId = mark.courseId;
                        existingMark.userId = Convert.ToInt32(Session["userId"]);
                        existingMark.exam_type = mark.exam_type;
                        existingMark.marks=mark.marks;
                        db.Entry(existingMark).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["ToastrMessage"] = "Mark updated successfully!";
                        TempData["ToastrType"] = "success";
                    }
                }
                else
                {
                    mark.userId= Convert.ToInt32(Session["userId"]);
                    mark.current_date= DateTime.Now;
                    db.Mark.Add(mark);
                    db.SaveChanges();
                    TempData["ToastrMessage"] = "Mark Saved successfully!";
                    TempData["ToastrType"] = "success";
                }
                return RedirectToAction("Mark");
            }
            return View(mark);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var mark = db.Mark.Find(id);
            if(mark != null)
            {
                db.Mark.Remove(mark);
                db.SaveChanges();
                TempData["ToastrMessage"] = "Mark Deleted successfully!";
                TempData["ToastrType"] = "success";
            }
            return RedirectToAction("Mark");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var mark = db.Mark.Find(id);
            if(mark == null)
            {
                return HttpNotFound();
            }
            ViewBag.Mark = db.Mark.ToList();
            ViewBag.StudentList = db.Student.ToList();
            ViewBag.CourseList = db.Course.ToList();
            return View("Mark", mark);
        }
    }
}