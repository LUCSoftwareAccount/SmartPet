using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartPet.Models;
using SmartPet.Repositories;

namespace SmartPet.Controllers
{
	public class PetController : Controller
	{
		private readonly IPetRepository _petRepository;

		public PetController()
		{
			_petRepository = new PetRepository(new SmartPetDbContext());
		}

		// GET: Pet
		public async Task<ActionResult> Index()
		{
			var pets = await _petRepository.GetAllPetsAsync();
			return View(pets);
		}

		// GET: Pet/Details/5
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var pet = await _petRepository.GetPetByIdAsync(id.Value);
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
		public async Task<ActionResult> Create(Pet pet)
		{
			if (Session["UserId"] == null)
			{
				return RedirectToAction("Login", "User");
			}

			pet.userId = Convert.ToInt32(Session["UserId"]);

			if (ModelState.IsValid)
			{
				await _petRepository.AddPetAsync(pet);
				return RedirectToAction("Dashboard", "Dashboard");
			}

			return View(pet);
		}


		// GET: Pet/Edit/5
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var pet = await _petRepository.GetPetByIdAsync(id.Value);
			if (pet == null)
			{
				return HttpNotFound();
			}

			return View(pet);
		}

		// POST: Pet/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, Pet pet)
		{
			if (id != pet.id)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			if (ModelState.IsValid)
			{
				await _petRepository.UpdatePetAsync(pet);
				return RedirectToAction("Dashboard", "Dashboard");
			}

			return View(pet);
		}

		// GET: Pet/Delete/5
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			var pet = await _petRepository.GetPetByIdAsync(id.Value);
			if (pet == null)
			{
				return HttpNotFound();
			}

			return View(pet);
		}

		// POST: Pet/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			await _petRepository.DeletePetAsync(id);
			return RedirectToAction("Dashboard", "Dashboard"); 
		}
		public PetController(IPetRepository petRepository)
		{
			_petRepository = petRepository;
		}
	}
}