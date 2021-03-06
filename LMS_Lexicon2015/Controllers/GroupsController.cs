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

        // GET: Skapar en sida med alla grupper
        public ActionResult Index(string sortOrder, string searchString)
        {
            {
                var groupIdInMenu = new LMS_Lexicon2015.Models.ApplicationDbContext().Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().GroupId;

                var studentIdWithPath = "Details/" + groupIdInMenu;

                ViewBag.OptionsDropDown = new SelectList(new[] { "Alla kurser", "Aktiva kurser" });

                if (User.IsInRole("Elev"))
                {
                    return RedirectToAction("Details/" + groupIdInMenu);
                }
                ViewBag.NameSortParm = sortOrder == "Name" ? "Name_desc" : "Name";
                ViewBag.DescriptionSortParm = sortOrder == "Description" ? "Description_desc" : "Description";
                ViewBag.StartDateSortParm = sortOrder == "StartDate" ? "StartDate_desc" : "StartDate";
                ViewBag.EndDateSortParm = sortOrder == "EndDate" ? "EndDate_desc" : "EndDate";

                ViewBag.searchString = searchString;

                LMS_Lexicon2015.Controllers.HomeController.HomeIndex = false;

                var Groups = from s in db.Groups select s;

                if (!String.IsNullOrEmpty(searchString))
                {
                    Groups = Groups.Where(s => s.Name.Contains(searchString)
                        //|| (s.EndDate.ToString()).Contains(searchString)
                        //|| (s.StartDate.ToString()).Contains(searchString)
                        //|| s.Description.Contains(searchString)
                     );
                }

                switch (sortOrder)
                {
                    case "Name_desc":
                        Groups = Groups.OrderByDescending(s => s.Name);
                        break;
                    case "Name":
                        Groups = Groups.OrderBy(s => s.Name);
                        break;

                    case "Description_desc":
                        Groups = Groups.OrderByDescending(s => s.Description);
                        break;
                    case "Description":
                        Groups = Groups.OrderBy(s => s.Description);
                        break;
                    case "StartDate_desc":
                        Groups = Groups.OrderByDescending(s => s.StartDate);
                        break;
                    case "StartDate":
                        Groups = Groups.OrderBy(s => s.StartDate);
                        break;
                    case "EndDate_desc":
                        Groups = Groups.OrderByDescending(s => s.EndDate);
                        break;
                    case "EndDate":
                        Groups = Groups.OrderBy(s => s.EndDate);
                        break;
                    default:
                        Groups = Groups.OrderByDescending(s => s.Name);
                        break;
                }

                var checkBox = Request.Form["ActiveChoise"];

                if (checkBox == "on")
                {
                    Groups = Groups.Where(o => o.EndDate > DateTime.Today && o.StartDate < DateTime.Today);
                }

                return View(Groups.ToList());
            }
        }





        //    return View(db.Groups.ToList());
        //}


        // GET: Skapar en sida med detaljerad information av en grupp
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
            ViewBag.Line3 = " Till ";
            ViewBag.GroupId = id;
            return View(group);
        }

        // GET: Skapae en sida från vilken man kan skapa en grupp
        [Authorize(Roles = "Lärare")]
        public ActionResult Create()
        {
             
            return View();
        }

        // POST: Skapar gruppen ovan
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Lärare")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Group group)
        {

            if (ModelState.IsValid)
            {
                if (group.StartDate.AddDays(1) < DateTime.Now)
                {

                    ModelState.AddModelError("", "Du har angivit ett startdatum före dagens datum");
                    return View(group);

                }

                else if (group.StartDate > group.EndDate)
                {

                    ModelState.AddModelError("", "Du har angivit ett slutdatum före startdatumet ");
                    return View(group);
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

        // GET: Skapar en sida från vilken man kan editera detaljinformationen av en grupp
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
    

            return View(group);
        }

        // POST: Sparar den editerade informationen
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Lärare")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,StartDate,EndDate")] Group group)
        {
            if (ModelState.IsValid)
            {

                if (group.StartDate > group.EndDate)
                {

                    ModelState.AddModelError("", "Du har angivit ett slutdatum före startdatumet ");
                    return View(group);
               }

                else
                {
                    int GroupNr = (int)group.Id;
                    db.Entry(group).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details/" + GroupNr, "Groups");
                }
            }
            return View(group);
        }

        // GET: Skapar en sida från vilken man kan ta bort en grupp
        [Authorize(Roles = "Lärare")]
        public ActionResult Delete(int? id)
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
            return View(group);
        }

        // POST: Tar bort gruppen angiven ovan
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

            var checkCourseOccasions = db.CourseOccasions.Where(o => o.GroupId == GroupId);
            string dbStartDate = "";

            foreach (var item in checkCourseOccasions)
            {
                dbStartDate = dbStartDate + "," + item.StartDate;

            }


            //ViewBag.dbStartDate = dbStartDate;
            //ViewBag.GroupId = GroupId;
            //return View(db.CourseOccasions.ToList());
            return View();
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
