using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Hangfire;
using SmartPet.Models;
using System.Linq; 

public class ReminderController : Controller
{
	private SmartPetDbContext db = new SmartPetDbContext();

	// GET: Reminder/Create
	public ActionResult Create()
	{
		var pets = db.Pets.ToList(); // Get all pets from the database
		var model = new ReminderViewModel
		{
			DueAt = DateTime.Now.AddHours(1),
			PetId = pets.Any() ? pets.First().id : 0 // Set a default petId, or 0 if no pets
		};

		ViewBag.Pets = new SelectList(pets, "id", "name", model.PetId); // Assuming Pet has Id and Name fields
		return View(model);
	}

	// POST: Reminder/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Create(ReminderViewModel model)
	{
		if (!ModelState.IsValid)
		{
			// repopulate the select list before re-displaying the view
			var pets = db.Pets.ToList();
			ViewBag.Pets = new SelectList(pets, "id", "name", model.PetId);
			return View(model);
		}

		var pet = db.Pets.Find(model.PetId);
		if (pet == null)
		{
			ModelState.AddModelError("", "Pet not found.");
			return View(model);
		}

		var username = User?.Identity?.Name;
		var user = !string.IsNullOrEmpty(username)
			? db.Users.FirstOrDefault(u => u.username == username)
			: null;

		if (user == null)
		{
			var sessionEmail = Session["UserEmail"] as string;
			if (!string.IsNullOrEmpty(sessionEmail))
				user = db.Users.FirstOrDefault(u => u.email == sessionEmail);
		}


		var reminder = new Reminder
		{
			petId = pet.id,
			message = model.Message,
			dueAt = model.TimeZoneId != null
				? TimeZoneInfo.ConvertTimeToUtc(model.DueAt, TimeZoneInfo.FindSystemTimeZoneById(model.TimeZoneId))
				: model.DueAt.ToUniversalTime(),
			userEmail = user.email,
			isSent = false
		};

		db.Reminders.Add(reminder);
		db.SaveChanges();

		BackgroundJob.Schedule(
			() => SendReminderEmailById(reminder.id),
			reminder.dueAt - DateTime.UtcNow
		);

		return RedirectToAction("Dashboard", "Dashboard");
	}

	// GET: Reminder/Edit/{id}
	public ActionResult Edit(int id)
	{
		var reminder = db.Reminders.Find(id);
		if (reminder == null)
		{
			return HttpNotFound();
		}

		// Populate the view model with the reminder data
		var model = new ReminderViewModel
		{
			PetId = reminder.petId,
			Message = reminder.message,
			DueAt = reminder.dueAt
		};

		// Load the list of pets for the dropdown (if you want to allow pet changes)
		ViewBag.Pets = new SelectList(db.Pets, "Id", "Name", reminder.petId);

		return View(model);
	}

	// POST: Reminder/Edit/{id}
	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Edit(int id, ReminderViewModel model)
	{
		if (!ModelState.IsValid)
			return View(model);

		var reminder = db.Reminders.Find(id);
		if (reminder == null)
		{
			return HttpNotFound();
		}

		// Update the reminder properties
		reminder.message = model.Message;
		reminder.dueAt = model.DueAt.ToUniversalTime();
		reminder.petId = model.PetId; // Keep pet tied to reminder

		db.SaveChanges();

		return RedirectToAction("Dashboard", "Dashboard");
	}

	// GET: Reminder/Delete/{id}
	public ActionResult Delete(int id)
	{
		var reminder = db.Reminders.Find(id);
		if (reminder == null)
		{
			return HttpNotFound();
		}

		return View(reminder);
	}

	// POST: Reminder/Delete/{id}
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public ActionResult DeleteConfirmed(int id)
	{
		var reminder = db.Reminders.Find(id);
		if (reminder != null)
		{
			db.Reminders.Remove(reminder);
			db.SaveChanges();
		}

		return RedirectToAction("Dashboard", "Dashboard");
	}

	public void SendReminderEmailById(int reminderId)
	{
		using (var db = new SmartPetDbContext())
		{
			var reminder = db.Reminders.Find(reminderId);

			if (reminder == null || reminder.isSent)
				return;

			// Guard against missing/empty email
			if (string.IsNullOrWhiteSpace(reminder.userEmail))
				return;

			var mail = new MailMessage();
			try
			{
				mail.To.Add(reminder.userEmail);
			}
			catch
			{
				// Invalid email format or other issue — abort sending.
				return;
			}

			mail.Subject = "Pet Reminder 🐾";
			mail.Body = reminder.message;
			mail.From = new MailAddress("sarahodish81@gmail.com");

			var smtp = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("saraodish81@gmail.com", "wtxomyzyrqrmbula"),
				EnableSsl = true,
			};

			try
			{
				smtp.Send(mail);
			}
			catch
			{
				// Sending failed — do not mark as sent; optionally log error.
				return;
			}

			reminder.isSent = true;
			db.SaveChanges();
		}
	}
}