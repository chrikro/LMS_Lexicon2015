using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LMS_Lexicon2015.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace LMS_Lexicon2015.Controllers
{
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
            return View(db.Documents.ToList());
        }

        // GET: Documents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // GET: Documents/Create
        public ActionResult Create()
        {
            ViewBag.Groupid = new SelectList(db.Groups, "Id", "Name");//en bäg för rullningslistan på formuläret
            return View();
        }
   

        //POST: Documents/Create
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //   public ActionResult Create([Bind(Include = "Id,Name,Url,Description,Timestamp,Deadline,UserId,GroupId,CourseOccasionId,ActivityId")] Document document, HttpPostedFileBase Name)

        public ActionResult Create([Bind(Include = "Id,Name,Description,Deadline,GroupId,CourseOccasionId,ActivityId")] Document document, HttpPostedFileBase Name)
        {

            if (ModelState.IsValid)
            {

                        string fileExtension = Name.FileName.Split('.').Last();
                        string fileName = Guid.NewGuid().ToString() + '.' + fileExtension;
                        var fileSavePath = Path.Combine(Server.MapPath("/Files"), fileName);
                        Name.SaveAs(fileSavePath);

                        var doc = new Document
                        {
                            Name = Name.FileName,
                            Url = fileSavePath,
                            Description = document.Description,
                            Timestamp = DateTime.Now,
                            Deadline = document.Deadline,
                            UserId = User.Identity.GetUserId(),
                            GroupId = document.GroupId,

                            CourseOccasionId = document.CourseOccasionId,
                            ActivityId = document.ActivityId
                        };

                        db.Documents.Add(doc);
                        db.SaveChanges();
                        return RedirectToAction("Index");
            }

            return View(document);
        }









        // GET: Documents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Url,Description,Timestamp,Deadline,UserId,GroupId,CourseOccasionId,ActivityId")] Document document)
        {
            if (ModelState.IsValid)
            {
                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(document);
        }

        // GET: Documents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Document document = db.Documents.Find(id);
            if (document == null)
            {
                return HttpNotFound();
            }
            return View(document);
        }

        // POST: Documents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public FileResult DownloadFile(string fileUrl, string fileName)
        {
            var filepath = System.IO.Path.Combine(Server.MapPath("/Files/"), fileUrl);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
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
