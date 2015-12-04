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
    [Authorize]
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            return View(db.Activitys.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id, int? id2, int? id3)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activitys.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.activitiesId = id;
            ViewBag.courseOccasionId = id2;
            ViewBag.groupId = id3;
            ViewBag.Line1 = "/";
            ViewBag.Line2 = "-";
            ViewBag.Line3 = " Till ";
           
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create(int? id, int? id2)
        {
            ViewBag.courseOccasionId = id;
            ViewBag.groupId = id2;
            ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 

             return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate,CourseId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activitys.Add(activity);
                db.SaveChanges();
                return RedirectToAction("Details/" + activity.CourseId + "/" + (int)TempData["GroupId"], "CourseOccasions");
            }

            ViewBag.courseOccasionId = activity.CourseId;
            ViewBag.groupId = (int)TempData["GroupId"];
            ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id, int? id2, int? id3)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activitys.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }

            ViewBag.activitiesId = id;
            ViewBag.courseOccasionId = id2;
            ViewBag.groupId = id3;
            string selectedId = activity.Name;
            ViewBag.name = new SelectList(db.ActivityTypes, "Name", "Name", selectedId);
            //ViewBag.name = new SelectList(db.ActivityTypes, "Name", "Name");

            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate,CourseId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                //return RedirectToAction("Index");
                return RedirectToAction("Details/" + activity.CourseId + "/" + (int)TempData["GroupId"], "CourseOccasions");
            }
            ViewBag.courseOccasionId = activity.CourseId;
            ViewBag.groupId = (int)TempData["GroupId"];
            ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activitys.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activitys.Find(id);
            db.Activitys.Remove(activity);
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
