﻿using System;
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
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public static bool ErrorMessageToEarly = false;
        public static bool ErrorMessageStartAfterEnd = false;

        // GET: Groups
        public ActionResult Index()
        {
            //ViewBag.userscount = db.Users.Where(gr;
            ViewBag.Line1 = "/";
            ViewBag.Line2 = "-";
            ErrorMessageToEarly = false;
            ErrorMessageStartAfterEnd = false;
            return View(db.Groups.ToList());
        }


        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.Line1 = "/";
            ViewBag.Line2 = "-";
            ViewBag.Line3 = " Till ";
            ViewBag.GroupId = id;
            return View(group);
        }

        // GET: Groups/Create
        [Authorize(Roles = "Lärare")]
        public ActionResult Create()
        {
            if (ErrorMessageToEarly == true)
            {
                ViewBag.ErrorMessage = "Du har angivit ett startdatum före dagens datum";
            }
            if (ErrorMessageStartAfterEnd == true)
            {
                ViewBag.ErrorMessage = "Du har angivit ett slutdatum före startdatumet ";
            }             
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Lärare")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Group group)
        {
            if (ModelState.IsValid)
            {
                if (group.StartDate < DateTime.Now)
                {
                    //AddErrors(ModelState);
                    ErrorMessageToEarly = true;
                    ErrorMessageStartAfterEnd = false;
                    return RedirectToAction("Create");
                }

                else if (group.StartDate > group.EndDate)
                {
                    //AddErrors(ModelState);
                    ErrorMessageStartAfterEnd = true;
                    ErrorMessageToEarly = false;
                    return RedirectToAction("Create");
                }

                else
                {
                    db.Groups.Add(group);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(group);
        }

        // GET: Groups/Edit/5
        [Authorize(Roles = "Lärare")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            if (ErrorMessageToEarly == true)
            {
                ViewBag.ErrorMessage = "Du har angivit ett startdatum före dagens datum";
            }
            if (ErrorMessageStartAfterEnd == true)
            {
                ViewBag.ErrorMessage = "Du har angivit ett slutdatum före startdatumet ";
            }         
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Lärare")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Group group)
        {
            if (ModelState.IsValid)
            {
                if (group.StartDate < DateTime.Now)
                {
                    //AddErrors(ModelState);
                    ErrorMessageToEarly = true;
                    ErrorMessageStartAfterEnd = false;
                    return RedirectToAction("Edit");
                }

                else if (group.StartDate > group.EndDate)
                {
                    //AddErrors(ModelState);
                    ErrorMessageStartAfterEnd = true;
                    ErrorMessageToEarly = false;
                    return RedirectToAction("Edit");
               }

                else
                {
                    db.Entry(group).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View(group);
        }

        // GET: Groups/Delete/5
        [Authorize(Roles = "Lärare")]
        public ActionResult Delete(int? id)
        {
            ViewBag.Line1 = "/";
            ViewBag.Line2 = "-";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [Authorize(Roles = "Lärare")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult _Course(int GroupId) 
        {
            //foreach (var Group in group.Courses)
            //{
            //    var courses = group.Courses.Where(c => c.GroupId == group.Id);
            //}

            //return View(courses);

            ViewBag.Line1 = "/";
            ViewBag.Line2 = "-";
            ViewBag.GroupId = GroupId;
            ErrorMessageToEarly = false;
            ErrorMessageStartAfterEnd = false;
            return View(db.CourseOccasions.ToList());
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
