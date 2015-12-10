using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VisionCollegeDSEDMarkingPortal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            ////Map route to AssignmentResultView
            //routes.MapRoute(
            //    name: "AssignmentResultView",
            //    url: "{controller}/{action}/{id1}/{id2}",
            //    defaults: new { controller = "AssignmentSubmission", action = "AssignmentResultView", id1 = UrlParameter.Optional, id2 = UrlParameter.Optional }
            //);


        }
    }
}
