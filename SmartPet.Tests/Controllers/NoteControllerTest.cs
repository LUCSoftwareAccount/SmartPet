using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPet.Controllers;
using SmartPet.Models;
using SmartPet.Tests.Fakes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartPet.Tests.Controllers
{
	[TestClass]
	public class NoteControllerTest
	{
		[TestMethod]
		public async Task Index_WithValidPetId_ReturnsNotesForThatPet()
		{
			var fakePetRepo = new FakePetRepository();
			var fakeNoteRepo = new FakeNoteRepository();
			var controller = new NoteController(fakeNoteRepo, fakePetRepo);

			var result = await controller.Index(1) as ViewResult;

			Assert.IsNotNull(result);

			var notes = ((IEnumerable<Note>)result.Model).ToList();

			Assert.AreEqual(1, notes.Count);
			Assert.AreEqual(1, notes[0].petId);
		}

		[TestMethod]
		public async Task Details_WithValidId_ReturnsNoteView()
		{
			var fakePetRepo = new FakePetRepository();
			var fakeNoteRepo = new FakeNoteRepository();
			var controller = new NoteController(fakeNoteRepo, fakePetRepo);

			var result = await controller.Details(1) as ViewResult;

			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result.Model, typeof(Note));

			var note = (Note)result.Model;
			Assert.AreEqual(1, note.id);
			Assert.AreEqual("First note", note.content);
		}

		[TestMethod]
		public void Create_Get_ReturnsView()
		{
			var fakePetRepo = new FakePetRepository();
			var fakeNoteRepo = new FakeNoteRepository();
			var controller = new NoteController(fakeNoteRepo, fakePetRepo);

			var result = controller.Create(1) as ViewResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public async Task DeleteConfirmed_RemovesNote_AndRedirects()
		{
			var fakePetRepo = new FakePetRepository();
			var fakeNoteRepo = new FakeNoteRepository();
			var controller = new NoteController(fakeNoteRepo, fakePetRepo);

			var result = await controller.DeleteConfirmed(1) as RedirectToRouteResult;

			Assert.IsNotNull(result);
			Assert.IsNull(fakeNoteRepo.Notes.FirstOrDefault(n => n.id == 1));

			// Your controller redirected to Dashboard last time
			Assert.AreEqual("Dashboard", result.RouteValues["action"]);
		}

		[TestMethod]
		public async Task Edit_Get_WithInvalidId_ReturnsBadRequest()
		{
			var fakePetRepo = new FakePetRepository();
			var fakeNoteRepo = new FakeNoteRepository();
			var controller = new NoteController(fakeNoteRepo, fakePetRepo);

			var result = await controller.Edit(null);

			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}

		[TestMethod]
		public async Task Delete_Get_WithInvalidId_ReturnsBadRequest()
		{
			var fakePetRepo = new FakePetRepository();
			var fakeNoteRepo = new FakeNoteRepository();
			var controller = new NoteController(fakeNoteRepo, fakePetRepo);

			var result = await controller.Delete(null);

			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}
	}
}
