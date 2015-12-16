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
    [Authorize]
    public class DocumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Documents
        public ActionResult Index()
        {
            var model = db.Documents.ToList();
            if (User.IsInRole("Elev"))
            {
                //var id = new LMS_Lexicon2015.Models.ApplicationDbContext().Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().GroupId;
                //model = db.Documents.Where(g => g.GroupId == id).ToList();
                var id = new LMS_Lexicon2015.Models.ApplicationDbContext().Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().Id;
                model = db.Documents.Where(g => g.UserId == id).ToList();
            }
            return View(model);
        }

        public ActionResult download()
        {


            // var Users = db.Users.Where(U => U.Id == r.GroupId).FirstOrDefault().Name;


            return View(db.Documents.ToList());
        }


        // GET: Documents/Details/5
         [Authorize(Roles = "Lärare")]
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

        // GET: Documents/upload
        public ActionResult upload(string id, int id2, int? id3, int? id4, string id5)
        {
            ViewBag.Groupid = new SelectList(db.Groups, "Id", "Name");//en bäg för rullningslistan på formuläret

            ViewBag.view = id;
            ViewBag.GroupId = id2;
            ViewBag.CourseOccasionId = id3;
            ViewBag.ActivityId = id4;
            ViewBag.AktivitetTyp = id5;
            return View();
        }


        //POST: Documents/upload
        //To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Create([Bind(Include = "Id,Name,Url,Description,Timestamp,Deadline,UserId,GroupId,CourseOccasionId,ActivityId")] Document document, HttpPostedFileBase Name)



        public ActionResult upload([Bind(Include = "Name,Description,GroupId,CourseOccasionId,ActivityId,Deadline")] CreateDocumentViewModel document, HttpPostedFileBase Name)
        {
            var manager = new UserManager<LMS_Lexicon2015.Models.ApplicationUser>(new Microsoft.AspNet.Identity.EntityFramework.UserStore<LMS_Lexicon2015.Models.ApplicationUser>(new LMS_Lexicon2015.Models.ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            if (ModelState.IsValid)
            {
                var view = (string)TempData["view"];
                var dbFileName = Name.FileName;
                string fileExtension = Name.FileName.Split('.').Last();

                string fileName = Guid.NewGuid().ToString() + '.' + fileExtension;
                var fileSavePath = Path.Combine(Server.MapPath("/Files"), fileName);
                Name.SaveAs(fileSavePath);


                if (view == "Activities" && User.IsInRole("Elev"))
                {
                    string fileNameElev = Name.FileName.Split('.').First();
                    dbFileName = fileNameElev + "-" + currentUser.FirstName + "-" + currentUser.LastName + '.' + fileExtension;
                }

                var doc = new Document
              {
                  //Name = Name.FileName,
                  Name = dbFileName,
                  //Url = fileSavePath,
                  Url = fileName,
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



                if (view == "Group")
                {
                    return RedirectToAction("Details/" + document.GroupId, "Groups");
                }
                else if (view == "Course")
                {
                    return RedirectToAction("Details/" + document.CourseOccasionId + "/" + document.GroupId, "CourseOccasions");
                }
                else if (view == "Activities")
                {
                    return RedirectToAction("Details/" + document.ActivityId + "/" + document.CourseOccasionId + "/" + document.GroupId, "Activities");


                }
                else
                {
                    return RedirectToAction("Index");
                }

            }

            ViewBag.GroupId = document.GroupId;
            ViewBag.CourseOccasionId = document.CourseOccasionId;
            ViewBag.view = (string)TempData["view"];

            return View(document);
        }




        // GET: Documents/Edit/5
         [Authorize(Roles = "Lärare")]
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
        [Authorize(Roles = "Lärare")]
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
         [Authorize(Roles = "Lärare")]
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
        [Authorize(Roles = "Lärare")]
        public ActionResult DeleteConfirmed(int id)
        {

            Document document = db.Documents.Find(id);

            var fileName = document.Url;
            var fileSavePath = Path.Combine(Server.MapPath("/Files"), fileName);

            FileInfo file = new FileInfo(fileSavePath);
            file.Delete();

            
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
