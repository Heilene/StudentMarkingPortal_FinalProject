using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisionCollegeDSEDMarkingPortal.DTO;
using VisionCollegeDSEDMarkingPortal.Models;
using VisionCollegeDSEDMarkingPortal.Models.ViewModels;
using VisionCollegeDSEDMarkingPortal.Repositories;

namespace VisionCollegeDSEDMarkingPortal.Controllers
{

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

      

        public  ActionResult MarkingViewModel()
        {  
            //instantiate classes
             DatabaseCalls myDatabaseCalls = new DatabaseCalls();
            MarkingVM myModel = new MarkingVM();
            

            //Viewbags
            ViewBag.Message = "Marking Schedule";

            //myModel.MyModules = myDatabaseCalls.GetModules();
            //myModel.MyStudents = myDatabaseCalls.GetStudents();


            return View(myModel);
        }

    }
}