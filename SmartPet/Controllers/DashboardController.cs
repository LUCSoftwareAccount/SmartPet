using System;
using System.Linq;
using System.Web.Mvc;
using SmartPet.Models;

namespace SmartPet.Controllers
{
	public class DashboardController : Controller
	{
		private SmartPetDbContext db = new SmartPetDbContext();

		public ActionResult Dashboard()
		{
			var model = new DashboardViewModel();

			model.TotalPets = db.Pets.Count();
			model.TotalReminders = db.Reminders.Count();
			model.TotalVaccines = db.Vaccines.Count();
			model.TotalNotes = db.Notes.Count();
			model.TotalMedicationLogs = db.MedicationLogs.Count();

			model.Pets = db.Pets
			  .OrderBy(p => p.name)
			  .ToList();

			model.UpcomingReminders = db.Reminders
			  .Where(r => r.dueAt >= DateTime.Now)
			  .OrderBy(r => r.dueAt)
			  .Take(5)
			  .ToList();

			model.RecentVaccines = db.Vaccines
			  .OrderByDescending(v => v.administeredOn)
			  .Take(5)
			  .ToList();

			model.RecentNotes = db.Notes
			  .OrderByDescending(n => n.createdAt)
			  .Take(5)
			  .ToList();

			
			ViewBag.MedicationLogs = db.MedicationLogs.ToList();

			return View(model);
		}
	}
}