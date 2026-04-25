using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using Hangfire;
using SmartPet.Models;

public class ReminderController : Controller
{
	private SmartPetDbContext db = new SmartPetDbContext();

	public ActionResult Create()
	{
		var model = new ReminderViewModel
		{
			DueAt = DateTime.Now.AddHours(1)
		};

		return View(model);
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public ActionResult Create(ReminderViewModel model)
	{
		if (!ModelState.IsValid)
			return View(model);

		var pet = db.Pets.Find(model.PetId);

		if (pet == null)
		{
			ModelState.AddModelError("", "Pet not found.");
			return View(model);
		}

		var reminder = new Reminder
		{
			petId = pet.id,
			message = model.Message,
			dueAt = model.DueAt.ToUniversalTime(),
			userEmail = User.Identity.Name,
			isSent = false
		};

		db.Reminders.Add(reminder);
		db.SaveChanges();

		BackgroundJob.Schedule(
			() => SendReminderEmailById(reminder.id),
			reminder.dueAt - DateTime.UtcNow
		);

		return RedirectToAction("Index", "Dashboard");
	}

	public void SendReminderEmailById(int reminderId)
	{
		using (var db = new SmartPetDbContext())
		{
			var reminder = db.Reminders.Find(reminderId);

			if (reminder == null || reminder.isSent)
				return;

			var mail = new MailMessage();
			mail.To.Add(reminder.userEmail);
			mail.Subject = "Pet Reminder 🐾";
			mail.Body = reminder.message;
			mail.From = new MailAddress("saraodish81@gmail.com");

			var smtp = new SmtpClient("smtp.gmail.com")
			{
				Port = 587,
				Credentials = new NetworkCredential("saraodish81@gmail.com", "wtxomyzyrqrmbula"),
				EnableSsl = true,
			};

			smtp.Send(mail);

			reminder.isSent = true;
			db.SaveChanges();
		}
	}
}