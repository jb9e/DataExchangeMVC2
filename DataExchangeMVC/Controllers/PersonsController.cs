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
	//[HandleError(Order=2)]
	public class PersonsController : Controller
	{
		private DataExchangeDBContext db = new DataExchangeDBContext();

		//
		// GET: /Persons/

		public ActionResult Index()
		{
			return View(db.Persons.ToList());
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
		[Authorize]
		public ActionResult Create(Person person)
		{
			if (ModelState.IsValid)
			{
				db.Persons.Add(person);
				db.SaveChanges();
				return RedirectToAction("Index");
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
		[Authorize(Users = "Administrator")]
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
		[Authorize(Users = "Administrator")]
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
			return View(db.Persons.ToList());
		}

		[HttpPost]
		[HandleError(View="NoRecordFoundError", ExceptionType=typeof(NoRecordFoundException))]
		[Authorize]
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
						  where p.FirstName == queryPerson.FirstName && p.LastName == queryPerson.LastName
						  select p;

			if (persons.Count() > 0)
			{
				return View(persons);
			}
			else
			{
				throw new NoRecordFoundException();
			}
		}
	}
}