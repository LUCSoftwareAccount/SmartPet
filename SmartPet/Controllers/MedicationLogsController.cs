using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SmartPet.Models;

namespace SmartPet.Controllers
{
	public class MedicationLogsController : Controller
	{
		private SmartPetDbContext db = new SmartPetDbContext();

		// GET: MedicationLogs
		public ActionResult Index(int? petId)
		{
			if (Session["UserId"] == null)
				return RedirectToAction("Login", "User");

			var logs = db.MedicationLogs
				.Include(m => m.Pet)
				.AsQueryable();

			if (petId.HasValue)
			{
				logs = logs.Where(l => l.petId == petId.Value);
			}

			logs = logs.OrderByDescending(m => m.administeredAt);

			return View(logs.ToList());
		}

		// GET: MedicationLogs/Create
		public ActionResult Create()
		{
			if (Session["UserId"] == null)
				return RedirectToAction("Login", "User");

			ViewBag.medicationId = new SelectList(db.Medications, "id", "name");
			ViewBag.petId = new SelectList(db.Pets, "id", "name");
			return View();
		}

		// POST: MedicationLogs/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "id,petId,medicationName,amount,administeredAt")] MedicationLogs log) 
		{
			if (Session["UserId"] == null) 
				return RedirectToAction("Login", "User");

			if (ModelState.IsValid) 
			{
				db.MedicationLogs.Add(log);
				db.SaveChanges();
				return RedirectToAction("Dashboard", "Dashboard");
			}

			ViewBag.medicationId = new SelectList(db.Medications, "id", "name", log.medicationId);
			ViewBag.petId = new SelectList(db.Pets, "id", "name", log.petId);
			return View(log);
		}

		// GET: MedicationLogs/Delete/5
		public ActionResult Delete(int? id)
		{
			if (Session["UserId"] == null)
				return RedirectToAction("Login", "User");

			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var log = db.MedicationLogs.Find(id);

			if (log == null)
				return HttpNotFound();

			return View(log);
		}

		// POST: MedicationLogs/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			var log = db.MedicationLogs.Find(id);
			db.MedicationLogs.Remove(log);
			db.SaveChanges();
			return RedirectToAction("Dashboard", "Dashboard");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				db.Dispose();
			base.Dispose(disposing);
		}
		public ActionResult Details(int? id)
		{
			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			var log = db.MedicationLogs.Find(id);
			if (log == null)
				return HttpNotFound();
			return View(log);
		}
		// GET: MedicationLogs/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			MedicationLogs log = db.MedicationLogs.Find(id);

			if (log == null)
			{
				return HttpNotFound();
			}

			return View(log);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "id,amount,administeredAt,medicationName")] MedicationLogs log)
		{
			if (ModelState.IsValid)
			{
				db.Entry(log).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Dashboard", "Dashboard");
			}

			return View(log);
		}

	}
}