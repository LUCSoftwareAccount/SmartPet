using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
using SmartPet.Models;

namespace SmartPet.Controllers
{
	public class PetController : Controller
	{
		private SmartPetDbContext db = new SmartPetDbContext();

		// GET: Pet
		public ActionResult Index()
		{
			return View(db.Pets.ToList());
		}

		// GET: Pet/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Pet pet = db.Pets.Find(id);

			if (pet == null)
			{
				return HttpNotFound();
			}

			return View(pet);
		}

		// GET: Pet/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: Pet/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Pet pet)
		{
			if (ModelState.IsValid)
			{
				db.Pets.Add(pet);
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(pet);
		}

		// GET: Pet/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Pet pet = db.Pets.Find(id);

			if (pet == null)
			{
				return HttpNotFound();
			}

			return View(pet);
		}

		// POST: Pet/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Pet pet)
		{
			if (ModelState.IsValid)
			{
				db.Entry(pet).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(pet);
		}

		// GET: Pet/Delete/5
		public ActionResult Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Pet pet = db.Pets.Find(id);

			if (pet == null)
			{
				return HttpNotFound();
			}

			return View(pet);
		}

		// POST: Pet/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public ActionResult DeleteConfirmed(int id)
		{
			Pet pet = db.Pets.Find(id);
			db.Pets.Remove(pet);
			db.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}