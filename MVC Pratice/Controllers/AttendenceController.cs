using MVC_Pratice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Pratice.Controllers
{
    public class AttendenceController : Controller
    {
        private AppDbContext db=new AppDbContext();
        // GET: Attendence
        public ActionResult Attendence()
        {
            var model = new Attendance
            {
                current_date = DateTime.Now,
            };
            ViewBag.Attendance = db.Attendance.
                Include(a=>a.User).
                Include(a=>a.Student).
                Include(a=>a.Course).
                ToList();

            ViewBag.StudentList = db.Student.ToList();
            ViewBag.CourseList = db.Course.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Attendance attendance)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login", "Index");
            }
            if (ModelState.IsValid)
            {
                if (attendance.Id > 0)
                {
                    var existingAttendence = db.Attendance.Find(attendance.Id);
                    if(existingAttendence != null)
                    {
                        existingAttendence.userId = Convert.ToInt32(Session["userId"]);
                        existingAttendence.current_date = attendance.current_date;
                        existingAttendence.studentId = attendance.studentId;
                        existingAttendence.courseId = attendance.courseId;
                        existingAttendence.date = attendance.date;
                        existingAttendence.status = attendance.status;
                        db.Entry(existingAttendence).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["ToastrMessage"] = "Attendence updated successfully!";
                        TempData["ToastrType"] = "success";
                    }

                }
                else
                {
                    attendance.userId = Convert.ToInt32(Session["userId"]);
                    attendance.current_date = DateTime.Now;
                    db.Attendance.Add(attendance);
                    db.SaveChanges();
                    TempData["ToastrMessage"] = "Attendence Saved successfully!";
                    TempData["ToastrType"] = "success";
                }
                return RedirectToAction("Attendence");
            }
            return View(attendance);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var attendance = db.Attendance.Find(id);
            if(attendance == null)
            {
                return HttpNotFound();
            }
            db.Attendance.Remove(attendance);
            db.SaveChanges();
            TempData["ToastrMessage"] = "Attendence Deleted successfully!";
            TempData["ToastrType"] = "success";
            return RedirectToAction("Attendence");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var attendance=db.Attendance.Find(id);
            if(attendance == null)
            {
                return HttpNotFound();
            }
            ViewBag.Attendance = db.Attendance.ToList();
            ViewBag.StudentList = db.Student.ToList();
            ViewBag.CourseList = db.Course.ToList();
            return View("Attendence", attendance);
        }
    }
}