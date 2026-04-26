using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using SmartPet.Models;

namespace SmartPet.Controllers
{
	public class ActivitiesController : Controller
	{
		private SmartPetDbContext db = new SmartPetDbContext();

		// GET: Activities
		public ActionResult Index()
		{
			var activities = db.Activities
				.Include(a => a.Pet)
				.OrderByDescending(a => a.activityDate)
				.ToList();

			return View(activities);
		}

		// GET: Activities/Create
		public ActionResult Create()
		{
			ViewBag.petId = new SelectList(db.Pets, "id", "name");
			return View();
		}

		// POST: Activities/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Activity activity)
		{
			if (ModelState.IsValid)
			{
				activity.createdAt = DateTime.Now;

				db.Activities.Add(activity);
				db.SaveChanges();

				return RedirectToAction("Index");
			}

			ViewBag.petId = new SelectList(db.Pets, "id", "name", activity.petId);
			return View(activity);
		}
	}
}