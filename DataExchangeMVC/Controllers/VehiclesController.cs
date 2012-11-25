using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataExchangeMVC.Models;
using DataExchangeMVC.Exceptions;
using System.Configuration;

namespace DataExchangeMVC.Controllers
{
    public class VehiclesController : Controller
    {
        private DataExchangeDBContext db = new DataExchangeDBContext();
        private LogsController _logsController = new LogsController();
        private int _configLogLevel = Convert.ToInt32(ConfigurationManager.AppSettings["LoggingLevel"]);

        //
        // GET: /Vehicles/

        public ActionResult Index()
        {
            return View(new List<Vehicle>());
        }

        //
        // GET: /Vehicles/Details/5

        public ActionResult Details(int id = 0)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        //
        // GET: /Vehicles/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Vehicles/Create

        [HttpPost]
        [MyAuthorize]
        public ActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                //return RedirectToAction("Index");
                ViewBag.VehicleEntryMessage = vehicle.Make + " " + vehicle.Model + " entered successfully!";
                if (_configLogLevel > 2)
                {
                    _logsController.LogMessage(3, "Successful entry for: " + vehicle.Make + " " + vehicle.Model + " by " + User.Identity.Name);
                }
                ModelState.Clear();
                return View();
            }

            return View(vehicle);
        }

        //
        // GET: /Vehicles/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        //
        // POST: /Vehicles/Edit/5

        [HttpPost]
        [MyAuthorize(Roles = "Admin", NotifyUrl = "/Account/NotAuthorized")]
        public ActionResult Edit(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        //
        // GET: /Vehicles/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        //
        // POST: /Vehicles/Delete/5

        [HttpPost, ActionName("Delete")]
        [MyAuthorize(Users = "Administrator", NotifyUrl = "/Account/NotAuthorized")]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult VehicleQuery()
        {
            return View(new List<Vehicle>());
        }

        [HttpPost]
        [HandleError(View = "NoRecordFoundError", ExceptionType = typeof(NoRecordFoundException))]
        [MyAuthorize]
        public ActionResult VehicleQuery(Vehicle queryVehicle)
        {
            Session["LastVehicleQuery"] = queryVehicle.Make + " " + queryVehicle.Model;

            var vehicles = from v in db.Vehicles
                          where v.Make.ToUpper().Contains(queryVehicle.Make.ToUpper()) && v.Model.ToUpper().Contains(queryVehicle.Model.ToUpper())
                          select v;

            if (vehicles.Count() > 0)
            {
                if (_configLogLevel > 2)
                {
                    _logsController.LogMessage(3, "Successful query for: " + queryVehicle.Make + " " + queryVehicle.Model);
                }
                return View(vehicles);
            }
            else
            {
                if (_configLogLevel > 1)
                {
                    _logsController.LogMessage(2, "No record found for: " + queryVehicle.Make + " " + queryVehicle.Model);
                }
                throw new NoRecordFoundException();
            }
        }
    }
}