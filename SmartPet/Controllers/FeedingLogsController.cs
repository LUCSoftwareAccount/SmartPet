using SmartPet.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SmartPet.Controllers
{
	public class FeedingLogsController : Controller
	{
		private SmartPetDbContext db = new SmartPetDbContext();

		public ActionResult Index()
		{
			return View(db.FeedingLogs.OrderByDescending(f => f.fedAt).ToList());
		}

		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Create(FeedingLogs feedingLog)
		{
			if (ModelState.IsValid)
			{
				if (feedingLog.fedAt == default(DateTime))
				{
					feedingLog.fedAt = DateTime.Now;
				}

				db.FeedingLogs.Add(feedingLog);
				db.SaveChanges();

				return RedirectToAction("Index");
			}

			return View(feedingLog);
		}

		public ActionResult Delete(int id)
		{
			var feedingLog = db.FeedingLogs.Find(id);

			if (feedingLog == null)
			{
				return HttpNotFound();
			}

			return View(feedingLog);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
			var feedingLog = db.FeedingLogs.Find(id);

			db.FeedingLogs.Remove(feedingLog);
			db.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}