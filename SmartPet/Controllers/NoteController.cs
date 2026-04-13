using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartPet.Models;
using SmartPet.Repositories;

namespace SmartPet.Controllers
{
	public class NoteController : Controller
	{
		private readonly INoteRepository _noteRepository;
		private readonly IPetRepository _petRepository;

		public NoteController()
		{
			var context = new SmartPetDbContext();
			_noteRepository = new NoteRepository(context);
			_petRepository = new PetRepository(context);
		}

		public async Task<ActionResult> Index(int? petId)
		{
			if (petId == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var pet = await _petRepository.GetPetByIdAsync(petId.Value);
			if (pet == null)
			{
				return HttpNotFound();
			}

			ViewBag.PetId = pet.id;
			ViewBag.PetName = pet.name;

			var notes = await _noteRepository.GetNotesByPetIdAsync(petId.Value);
			return View(notes);
		}

		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var note = await _noteRepository.GetNoteByIdAsync(id.Value);
			if (note == null)
			{
				return HttpNotFound();
			}

			return View(note);
		}

		public ActionResult Create(int petId)
		{
			var note = new Note
			{
				petId = petId
			};

			return View(note);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create(Note note)
		{
			if (ModelState.IsValid)
			{
				await _noteRepository.AddNoteAsync(note);
				return RedirectToAction("Index", new { petId = note.petId });
			}

			return View(note);
		}

		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var note = await _noteRepository.GetNoteByIdAsync(id.Value);
			if (note == null)
			{
				return HttpNotFound();
			}

			return View(note);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, Note note)
		{
			if (id != note.id)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			if (ModelState.IsValid)
			{
				await _noteRepository.UpdateNoteAsync(note);
				return RedirectToAction("Index", new { petId = note.petId });
			}

			return View(note);
		}

		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var note = await _noteRepository.GetNoteByIdAsync(id.Value);
			if (note == null)
			{
				return HttpNotFound();
			}

			return View(note);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			var note = await _noteRepository.GetNoteByIdAsync(id);
			if (note == null)
			{
				return HttpNotFound();
			}

			int petId = note.petId;
			await _noteRepository.DeleteNoteAsync(id);

			return RedirectToAction("Index", new { petId = petId });
		}
	}
}
