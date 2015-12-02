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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Om Lexicon AB";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Kontaktsida";

            return View();
        }
    }
}