using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataExchangeMVC.Models;

namespace DataExchangeMVC.Controllers
{
    public class LogsController : Controller
    {
        private DataExchangeDBContext db = new DataExchangeDBContext();

        //
        // GET: /Logs/

        [MyAuthorize(Roles = "Admin", NotifyUrl = "/Account/NotAuthorized")]
        public ActionResult Index()
        {
            return View(db.Logs.ToList());
        }

        //
        // GET: /Logs/Details/5

        public ActionResult Details(int id = 0)
        {
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        //
        // GET: /Logs/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Logs/Create

        [HttpPost]
        public ActionResult Create(Log log)
        {
            if (ModelState.IsValid)
            {
                db.Logs.Add(log);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(log);
        }

        //
        // GET: /Logs/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        //
        // POST: /Logs/Edit/5

        [HttpPost]
        [MyAuthorize(Roles = "Admin", NotifyUrl = "/Account/NotAuthorized")]
        public ActionResult Edit(Log log)
        {
            if (ModelState.IsValid)
            {
                db.Entry(log).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(log);
        }

        //
        // GET: /Logs/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Log log = db.Logs.Find(id);
            if (log == null)
            {
                return HttpNotFound();
            }
            return View(log);
        }

        //
        // POST: /Logs/Delete/5

        [HttpPost, ActionName("Delete")]
        [MyAuthorize(Users = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Log log = db.Logs.Find(id);
            db.Logs.Remove(log);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public bool LogMessage(int loggingLevel, string message)
        {
            try
            {
                Log log = new Log();

                log.Level = loggingLevel;
                log.Message = message;
                log.Time = DateTime.Now;

                db.Logs.Add(log);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Logging Failed!! " + ex.Message);
                return false;
            }
        }
    }
}