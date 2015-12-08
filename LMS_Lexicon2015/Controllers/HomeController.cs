using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;


namespace LMS_Lexicon2015.Controllers
{
    public class HomeController : Controller
    {
        public static bool HomeIndex = false;
        public ActionResult Index()
        {

            
            var groupId =  new LMS_Lexicon2015.Models.ApplicationDbContext().Users.Where(u => u.Email == User.Identity.Name).FirstOrDefault().GroupId;
            ViewBag.groupId = groupId;
            ViewBag.courseOccasionId = new LMS_Lexicon2015.Models.ApplicationDbContext().CourseOccasions.Where(o => o.GroupId == (int)groupId && o.StartDate < DateTime.Today && o.EndDate > DateTime.Today).First().Activities.First().CourseId;
  
            HomeIndex = true;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}