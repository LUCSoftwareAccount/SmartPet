using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPet.Controllers;
using SmartPet.Models;
using SmartPet.Tests.Fakes;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SmartPet.Tests.Controllers
{
	[TestClass]
	public class PetControllerTest
	{
		[TestMethod]
		public async Task Details_WithValidId_ReturnsPetView()
		{
			var fakeRepo = new FakePetRepository();
			var controller = new PetController(fakeRepo);

			var result = await controller.Details(1) as ViewResult;

			Assert.IsNotNull(result);
			Assert.IsInstanceOfType(result.Model, typeof(Pet));

			var pet = (Pet)result.Model;
			Assert.AreEqual(1, pet.id);
			Assert.AreEqual("Milo", pet.name);
		}

		[TestMethod]
		public async Task Details_WithInvalidId_ReturnsNotFound()
		{
			var fakeRepo = new FakePetRepository();
			var controller = new PetController(fakeRepo);

			var result = await controller.Details(999);

			Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
		}

		[TestMethod]
		public void Create_Get_ReturnsView()
		{
			var fakeRepo = new FakePetRepository();
			var controller = new PetController(fakeRepo);

			var result = controller.Create() as ViewResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public async Task DeleteConfirmed_RemovesPet()
		{
			var fakeRepo = new FakePetRepository();
			var controller = new PetController(fakeRepo);

			var result = await controller.DeleteConfirmed(1) as RedirectToRouteResult;

			Assert.IsNotNull(result);
			Assert.AreEqual(1, fakeRepo.Pets.Count);
			Assert.IsNull(fakeRepo.Pets.FirstOrDefault(p => p.id == 1));
		}

		[TestMethod]
		public async Task Edit_Get_WithInvalidId_ReturnsBadRequest()
		{
			var fakeRepo = new FakePetRepository();
			var controller = new PetController(fakeRepo);

			var result = await controller.Edit(null);

			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}

		[TestMethod]
		public async Task Delete_Get_WithInvalidId_ReturnsBadRequest()
		{
			var fakeRepo = new FakePetRepository();
			var controller = new PetController(fakeRepo);

			var result = await controller.Delete(null);

			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}
	}
}
