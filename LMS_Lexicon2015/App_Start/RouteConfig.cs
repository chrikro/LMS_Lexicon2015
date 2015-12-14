using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LMS_Lexicon2015
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //name: "CourseDetails",
            //url: "CourseOccasions/Details/{id}/{id2}",
            //defaults: new { controller = "CourseOccasions", action = "Details", id = UrlParameter.Optional, id2 = UrlParameter.Optional });


            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}/{id2}/{id3}",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional, id3 = UrlParameter.Optional });
            
           routes.MapRoute(
           name: "aktivityDocument",
           url: "{controller}/{action}/{id}/{id2}/{id3}/{id4}",
           defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, id2 = UrlParameter.Optional, id3 = UrlParameter.Optional, id4 = UrlParameter.Optional });

            //routes.MapRoute(
            //name: "Default",
            //url: "{controller}/{action}/{id}/{siteView}",
            //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, siteView = UrlParameter.Optional });
 

            //routes.MapRoute(
            //name: "Default",
            //url: "{controller}/{action}/{id}/{siteView}",
            //defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, siteView = UrlParameter.Optional });

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}

