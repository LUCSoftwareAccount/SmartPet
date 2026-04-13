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

			model.RecentMedicationLogs = (
			  from log in db.MedicationLogs
			  join med in db.Medications on log.medicationId equals med.id
			  orderby log.administeredAt descending
			  select new DashboardMedicationLogItem
			  {
				  LogId = log.id,
				  MedicationName = med.name,
				  Amount = log.amount,
				  AdministeredAt = log.administeredAt,
				  PetId = med.petId
			  }
			).Take(5).ToList();

			return View(model);
		}
	}
}