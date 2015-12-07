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
            HomeIndex = true;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}