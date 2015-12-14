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

            ViewBag.NoActivities = "Inga aktiviteter listas här.";
            return View(db.Activitys.ToList()); //vyn tom gör inget om kvar

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
        [Authorize(Roles = "Lärare")]
        public ActionResult Create(int? id, int? id2)
        {
            ViewBag.courseOccasionId = id;
            ViewBag.groupId = id2;
            ViewBag.StartDate = DateTime.Now;
            ViewBag.EndDate = DateTime.Now;

            DateTime StartDate = DateTime.Now;
            TimeSpan tStartDate = new TimeSpan(09, 00, 0);
            StartDate = StartDate.Date + tStartDate;

            DateTime EndDate = DateTime.Now;
            TimeSpan tEndDate = new TimeSpan(17, 00, 0);
            EndDate = EndDate.Date + tEndDate;



            ViewBag.StartDate = StartDate;
            ViewBag.EndDate = EndDate;

                ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 

             return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Lärare")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate,CourseId")] Activity activity)
        {
            if (ModelState.IsValid)
            {

                DateTime CoursesStartDate = db.CourseOccasions.Where(c => c.Id == activity.CourseId).FirstOrDefault().StartDate;
                DateTime CoursesEndDate = db.CourseOccasions.Where(c => c.Id == activity.CourseId).FirstOrDefault().EndDate;

                if (activity.StartDate < CoursesStartDate)
                {
                    //AddErrors(ModelState);
                    ViewBag.courseOccasionId = activity.CourseId;
                    ViewBag.groupId = (int)TempData["GroupId"];
                    ViewBag.StartDate = activity.StartDate;
                    ViewBag.EndDate = activity.EndDate;
                    ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
                    ModelState.AddModelError("", "Du har angivit ett startdatumet före kursens startdatumet");
                    return View(activity);
                }

                else if (activity.EndDate > CoursesEndDate)
                {
                    //AddErrors(ModelState);
                    ViewBag.courseOccasionId = activity.CourseId;
                    ViewBag.groupId = (int)TempData["GroupId"];
                    ViewBag.StartDate = activity.StartDate;
                    ViewBag.EndDate = activity.EndDate;
                    ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
                    ModelState.AddModelError("", "Du har angivit ett slutdatum efter kursens slutdatum");
                    return View(activity);
                }
                else if (activity.StartDate > activity.EndDate)
                {
                    //AddErrors(ModelState);
                    ViewBag.courseOccasionId = activity.CourseId;
                    ViewBag.groupId = (int)TempData["GroupId"];
                    ViewBag.StartDate = activity.StartDate;
                    ViewBag.EndDate = activity.EndDate;
                    ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
                    ModelState.AddModelError("", "Du har angivit ett slutdatum före startdatumet ");
                    return View(activity);
                }

                else
                {
                    db.Activitys.Add(activity);
                    db.SaveChanges();
                    return RedirectToAction("Details/" + activity.CourseId + "/" + (int)TempData["GroupId"], "CourseOccasions");
                }
            }

            ViewBag.courseOccasionId = activity.CourseId;
            ViewBag.groupId = (int)TempData["GroupId"];
            ViewBag.StartDate = activity.StartDate;
            ViewBag.EndDate = activity.EndDate;
            ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
            return View(activity);
        }

        // GET: Activities/Edit/5
        [Authorize(Roles = "Lärare")]
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


            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Lärare")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate,CourseId")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                DateTime CoursesStartDate = db.CourseOccasions.Where(c => c.Id == activity.CourseId).FirstOrDefault().StartDate;
                DateTime CoursesEndDate = db.CourseOccasions.Where(c => c.Id == activity.CourseId).FirstOrDefault().EndDate;

                if (activity.StartDate < CoursesStartDate)
                {
                    //AddErrors(ModelState);
                    ViewBag.courseOccasionId = activity.CourseId;
                    ViewBag.groupId = (int)TempData["GroupId"];
                    ViewBag.StartDate = activity.StartDate;
                    ViewBag.EndDate = activity.EndDate;
                    ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
                    ModelState.AddModelError("", "Du har angivit ett startdatumet före kursens startdatumet");
                    return View(activity);
                }

                else if (activity.EndDate > CoursesEndDate)
                {
                    //AddErrors(ModelState);
                    ViewBag.courseOccasionId = activity.CourseId;
                    ViewBag.groupId = (int)TempData["GroupId"];
                    ViewBag.StartDate = activity.StartDate;
                    ViewBag.EndDate = activity.EndDate;
                    ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
                    ModelState.AddModelError("", "Du har angivit ett slutdatum efter kursens slutdatum");
                    return View(activity);
                }
                else if (activity.StartDate > activity.EndDate)
                {
                    //AddErrors(ModelState);
                    ViewBag.courseOccasionId = activity.CourseId;
                    ViewBag.groupId = (int)TempData["GroupId"];
                    ViewBag.StartDate = activity.StartDate;
                    ViewBag.EndDate = activity.EndDate;
                    ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
                    ModelState.AddModelError("", "Du har angivit ett slutdatum före startdatumet ");
                    return View(activity);
                }

                else
                {
                    db.Entry(activity).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details/" + activity.CourseId + "/" + (int)TempData["GroupId"], "CourseOccasions");
                }


            }
            ViewBag.courseOccasionId = activity.CourseId;
            ViewBag.groupId = (int)TempData["GroupId"];
            ViewBag.StartDate = activity.StartDate;
            ViewBag.EndDate = activity.EndDate;
            ViewBag.Name = new SelectList(db.ActivityTypes, "Name", "Name");//en bäg för rullningslistan på formuläret 
            return View(activity);
        }

        // GET: Activities/Delete/5
        [Authorize(Roles = "Lärare")]
        public ActionResult Delete(int? id, int? id2, int? id3)
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

        // POST: Activities/Delete/5
        [Authorize(Roles = "Lärare")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Activity activity = db.Activitys.Find(id);
            //db.Activitys.Remove(activity);
            //db.SaveChanges();
            //return RedirectToAction("Index");

            // return RedirectToAction("Details/" + activity.CourseId + "/" + (int)TempData["GroupId"], "CourseOccasions");


            Activity activity = db.Activitys.Find(id);
            int CourseId = (int)activity.CourseId;
            db.Activitys.Remove(activity);
            db.SaveChanges();
            //return RedirectToAction("Details/" + CourseId, "CourseOccasions");
            return RedirectToAction("Details/" + CourseId + "/" + (int)TempData["GroupId"], "CourseOccasions");

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
