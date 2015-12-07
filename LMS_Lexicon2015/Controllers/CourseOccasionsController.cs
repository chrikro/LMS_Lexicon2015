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
    public class CourseOccasionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static bool ErrorMessageStartAfterEnd = false;
        // GET: CourseOccasions
        public ActionResult Index()
        {
            ErrorMessageStartAfterEnd = false;
            return View(db.CourseOccasions.ToList());
        }

        // GET: CourseOccasions/Details/5
        public ActionResult Details(int? id, int? id2)
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
            ViewBag.Line3 = " Till ";
            ViewBag.courseOccasionId = id;
            ViewBag.groupId = id2;
            return View(courseOccasion);
        }

        // GET: CourseOccasions/Create
        [Authorize(Roles = "Lärare")]
        public ActionResult Create(int? id)
        {
            if (ErrorMessageStartAfterEnd == true)
            {
                ViewBag.ErrorMessage = "Du har angivit ett slutdatum före startdatumet ";
            }
            ViewBag.GroupId = id;
            return View();
        }

        // POST: CourseOccasions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Lärare")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate,GroupId")] CourseOccasion courseOccasion)
        {
            if (ModelState.IsValid)
            {
                ErrorMessageStartAfterEnd = false;
                if (courseOccasion.StartDate > courseOccasion.EndDate)
                {
                    //AddErrors(ModelState);
                    ErrorMessageStartAfterEnd = true;
                    return RedirectToAction("Create");
                }

                else
                {
                    db.CourseOccasions.Add(courseOccasion);
                    db.SaveChanges();
                    return RedirectToAction("Details/" + (int)courseOccasion.GroupId, "Groups");
                }


                //db.CourseOccasions.Add(courseOccasion);
                //db.SaveChanges();
                //return RedirectToAction("Details/" + (int)courseOccasion.GroupId, "Groups");
            }
            return View(courseOccasion);
        }



        // GET: CourseOccasions/Edit/5
        [Authorize(Roles = "Lärare")]
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
            ViewBag.GroupId = id;  //fel
            if (ErrorMessageStartAfterEnd == true)
            {
                ViewBag.ErrorMessage = "Du har angivit ett slutdatum före startdatumet ";
            }      
            return View(courseOccasion);
        }

        // POST: CourseOccasions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Lärare")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate,GroupId")] CourseOccasion courseOccasion)
        {
            if (ModelState.IsValid)
            {
                ErrorMessageStartAfterEnd = false;
                if (courseOccasion.StartDate > courseOccasion.EndDate)
                {
                    //AddErrors(ModelState);
                    ErrorMessageStartAfterEnd = true;
                    return RedirectToAction("Edit");
                }

                else
                {
                    db.Entry(courseOccasion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details/" + (int)courseOccasion.Id + "/" + (int)courseOccasion.GroupId, "CourseOccasions");
                }
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
            ViewBag.Line1 = "/";
            ViewBag.Line2 = "-";
            ViewBag.Line3 = " Till ";
            return View(courseOccasion);
        }

        [Authorize(Roles = "Lärare")]
        // POST: CourseOccasions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseOccasion courseOccasion = db.CourseOccasions.Find(id);
            int GroupNr = (int)courseOccasion.GroupId;
            db.CourseOccasions.Remove(courseOccasion);
            db.SaveChanges();
            return RedirectToAction("Details/" + GroupNr, "Groups");
            //return RedirectToAction("Index");
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
