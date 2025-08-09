using MVC_Pratice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Pratice.Controllers
{
    public class BatchController : Controller
    {
        private AppDbContext db = new AppDbContext();
        // GET: Batch
        public ActionResult Batch()
        {
            var model = new Batch
            {
                current_date = DateTime.Now,
            };
            ViewBag.Batch = db.Batch.
                Include(b=>b.User).
                ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(Batch batch)
        {
            if (Session["userId"] == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if (ModelState.IsValid)
            {
                if(batch.Id > 0)
                {
                    var existingBatch = db.Batch.Find(batch.Id);
                    if (existingBatch != null)
                    {
                        existingBatch.current_date=batch.current_date;
                        existingBatch.batch_name = batch.batch_name;
                        existingBatch.userId = Convert.ToInt32(Session["userId"]);
                        db.Entry(existingBatch).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["ToastrMessage"] = "Batch updated successfully!";
                        TempData["ToastrType"] = "success";
                    }

                }
                else
                {
                    batch.userId=Convert.ToInt32(Session["userId"]);
                    batch.current_date= DateTime.Now;
                    db.Batch.Add(batch);
                    db.SaveChanges();
                    TempData["ToastrMessage"] = "Batch Saved successfully!";
                    TempData["ToastrType"] = "success";
                }

                return RedirectToAction("Batch");
            }
            ViewBag.Batch = db.Batch.ToList();
            return View("Batch",batch);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var batch = db.Batch.Find(id);
            if(batch != null)
            {
                db.Batch.Remove(batch);
                db.SaveChanges();
                TempData["ToastrMessage"] = "Batch Delete successfully!";
                TempData["ToastrType"] = "success";
            }
            return RedirectToAction("Batch");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var batch = db.Batch.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.Batch = db.Batch.ToList();
            return View("Batch", batch);
        }

    }
}