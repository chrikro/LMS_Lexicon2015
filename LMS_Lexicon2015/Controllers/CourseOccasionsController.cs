using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS_Lexicon2015.Models;

namespace LMS_Lexicon2015.Controllers
{
    public class CourseOccasionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CourseOccasions
        public ActionResult Index()
        {
            return View(db.CourseOccasions.ToList());
        }

        // GET: CourseOccasions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOccasion courseOccasion = db.CourseOccasions.Find(id);
            if (courseOccasion == null)
            {
                return HttpNotFound();
            }
            return View(courseOccasion);
        }

        // GET: CourseOccasions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseOccasions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Namn,Description,StartDate,EndDate,GroupId")] CourseOccasion courseOccasion)
        {
            if (ModelState.IsValid)
            {
                db.CourseOccasions.Add(courseOccasion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseOccasion);
        }

        // GET: CourseOccasions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOccasion courseOccasion = db.CourseOccasions.Find(id);
            if (courseOccasion == null)
            {
                return HttpNotFound();
            }
            return View(courseOccasion);
        }

        // POST: CourseOccasions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Namn,Description,StartDate,EndDate,GroupId")] CourseOccasion courseOccasion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseOccasion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseOccasion);
        }

        // GET: CourseOccasions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseOccasion courseOccasion = db.CourseOccasions.Find(id);
            if (courseOccasion == null)
            {
                return HttpNotFound();
            }
            return View(courseOccasion);
        }

        // POST: CourseOccasions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseOccasion courseOccasion = db.CourseOccasions.Find(id);
            db.CourseOccasions.Remove(courseOccasion);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
