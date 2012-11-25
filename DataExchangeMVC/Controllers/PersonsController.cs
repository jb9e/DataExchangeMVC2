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
	//[HandleError(Order=2)]
	public class PersonsController : Controller
	{
		private DataExchangeDBContext db = new DataExchangeDBContext();
		private LogsController _logsController = new LogsController();
		private int _configLogLevel = Convert.ToInt32(ConfigurationManager.AppSettings["LoggingLevel"]);

		//
		// GET: /Persons/

		public ActionResult Index()
		{
			return View(new List<Person>());
		}

		//
		// GET: /Persons/Details/5

		public ActionResult Details(int id = 0)
		{
			Person person = db.Persons.Find(id);
			if (person == null)
			{
				return HttpNotFound();
			}
			return View(person);
		}

		//
		// GET: /Persons/Create

		public ActionResult Create()
		{
			return View();
		}

		//
		// POST: /Persons/Create

		[HttpPost]
		//[MyAuthorize]
		public ActionResult Create(Person person)
		{
			if (ModelState.IsValid)
			{
				db.Persons.Add(person);
				db.SaveChanges();
				//return RedirectToAction("Index");
				ViewBag.PersonEntryMessage = person.FirstName + " " + person.LastName + " entered successfully!";
				if (_configLogLevel > 2)
				{
					_logsController.LogMessage(3, "Successful entry for: " + person.FirstName + " " + person.LastName + " by " + User.Identity.Name);
				}
				ModelState.Clear();
				return View();
			}

			return View(person);
		}

		//
		// GET: /Persons/Edit/5

		public ActionResult Edit(int id = 0)
		{
			Person person = db.Persons.Find(id);
			if (person == null)
			{
				return HttpNotFound();
			}
			return View(person);
		}

		//
		// POST: /Persons/Edit/5

		[HttpPost]
		[MyAuthorize(Roles = "Admin", NotifyUrl = "/Account/NotAuthorized")]
		public ActionResult Edit(Person person)
		{
			if (ModelState.IsValid)
			{
				db.Entry(person).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(person);
		}

		//
		// GET: /Persons/Delete/5

		public ActionResult Delete(int id = 0)
		{
			Person person = db.Persons.Find(id);
			if (person == null)
			{
				return HttpNotFound();
			}
			return View(person);
		}

		//
		// POST: /Persons/Delete/5

		[HttpPost, ActionName("Delete")]
		[MyAuthorize(Roles = "Admin", NotifyUrl = "/Account/NotAuthorized")]
		public ActionResult DeleteConfirmed(int id)
		{
			Person person = db.Persons.Find(id);
			db.Persons.Remove(person);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}

		public ActionResult PersonQuery()
		{
			return View(new List<Person>());
		}

		[HttpPost]
		[HandleError(View="NoRecordFoundError", ExceptionType=typeof(NoRecordFoundException))]
		//[MyAuthorize]
		public ActionResult PersonQuery(Person queryPerson)
		{
			//var FirstNameLst = new List<string>();

			//var FirstNameQry = from d in db.Persons
			//               orderby d.FirstName
			//               select d.FirstName;
			//FirstNameLst.AddRange(FirstNameQry.Distinct());
			//ViewBag.personFirstName = new SelectList(FirstNameLst);

			//var persons = from p in db.Persons
			//             select p;

			//if (!string.IsNullOrEmpty(lastName))
			//{
			//    persons = persons.Where(s => s.LastName.Contains(lastName));
			//}

			Session["LastPersonQuery"] = queryPerson.FirstName + " " + queryPerson.LastName;
			
			var persons = from p in db.Persons
						  where p.FirstName.ToUpper().Contains(queryPerson.FirstName.ToUpper()) && p.LastName.ToUpper().Contains(queryPerson.LastName.ToUpper())
						  select p;

			if (persons.Count() > 0)
			{
				if (_configLogLevel > 2)
				{
					_logsController.LogMessage(3, "Successful query for: " + queryPerson.FirstName + " " + queryPerson.LastName);
				}
				return View(persons);
			}
			else
			{
				if (_configLogLevel > 1)
				{					
					_logsController.LogMessage(2, "No record found for: " + queryPerson.FirstName + " " + queryPerson.LastName); 
				}
				throw new NoRecordFoundException();
			}
		}
	}
}