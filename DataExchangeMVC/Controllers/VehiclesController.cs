using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataExchangeMVC.Models;
using DataExchangeMVC.Exceptions;

namespace DataExchangeMVC.Controllers
{
    public class VehiclesController : Controller
    {
        private DataExchangeDBContext db = new DataExchangeDBContext();

        //
        // GET: /Vehicles/

        public ActionResult Index()
        {
            return View(db.Vehicles.ToList());
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
        [Authorize]
        public ActionResult Create(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
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
        [Authorize(Users = "Administrator")]
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
        [Authorize(Users = "Administrator")]
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
            return View(db.Vehicles.ToList());
        }

        [HttpPost]
        [HandleError(View = "NoRecordFoundError", ExceptionType = typeof(NoRecordFoundException))]
        [Authorize]
        public ActionResult VehicleQuery(Vehicle queryVehicle)
        {
            Session["LastVehicleQuery"] = queryVehicle.Make + " " + queryVehicle.Model;

            var vehicles = from v in db.Vehicles
                          where v.Make == queryVehicle.Make && v.Model == queryVehicle.Model
                          select v;

            if (vehicles.Count() > 0)
            {
                return View(vehicles);
            }
            else
            {
                throw new NoRecordFoundException();
            }
        }
    }
}