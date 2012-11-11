using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DataExchangeMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the Data Exchange Tool.";
            if (HttpContext != null && HttpContext.Application != null && HttpContext.Application["SiteCounter"] != null)
            {
                ViewBag.NumTimes = HttpContext.Application["SiteCounter"]; 
            }

            if (Session != null && Session["LastPersonQuery"] != null)
            {
                ViewBag.LastPersonQuery = "The last person queried was: " + Session["LastPersonQuery"]; 
            }
            else
            {
                ViewBag.LastPersonQuery = "No person queries attempted yet.";
            }

            if (Session != null && Session["LastVehicleQuery"] != null)
            {
                ViewBag.LastVehicleQuery = "The last vehicle queried was: " + Session["LastVehicleQuery"];
            }
            else
            {
                ViewBag.LastVehicleQuery = "No vehicle queries attempted yet.";
            }

            return View();
        }

        //public ActionResult Welcome(string name, int numTimes = 1)
        //{
        //    ViewBag.Message = "Hello " + name;
        //    ViewBag.NumTimes = numTimes;

        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "This tool does not do much.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact information.";

            return View();
        }
    }
}
