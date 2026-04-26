using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPet.Controllers;
using SmartPet.Models;
using SmartPet.Tests.Fakes;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace SmartPet.Tests.Controllers
{
	[TestClass]
	public class UserControllerTest
	{
		private UserController GetController(FakeUserRepository fakeRepo)
		{
			var controller = new UserController(fakeRepo);

			controller.ControllerContext = new ControllerContext(
				new FakeHttpContext(),
				new RouteData(),
				controller
			);

			return controller;
		}

		// -------------------------------
		// LOGIN TESTS
		// -------------------------------

		[TestMethod]
		public async Task Login_WithValidCredentials_RedirectsToDashboard()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var form = new FormCollection
			{
				{ "Email", "test@test.com" },
				{ "Password", "1234" }
			};

			var result = await controller.Login(form) as RedirectToRouteResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Dashboard", result.RouteValues["action"]);
			Assert.AreEqual("Dashboard", result.RouteValues["controller"]);
		}

		[TestMethod]
		public async Task Login_WithInvalidPassword_ReturnsError()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var form = new FormCollection
			{
				{ "Email", "test@test.com" },
				{ "Password", "wrongpass" }
			};

			var result = await controller.Login(form) as ViewResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Invalid email or password.", controller.ViewBag.Error);
		}

		[TestMethod]
		public async Task Login_WithUnverifiedUser_ReturnsError()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var form = new FormCollection
			{
				{ "Email", "unverified@test.com" },
				{ "Password", "1234" }
			};

			var result = await controller.Login(form) as ViewResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Please verify your email first.", controller.ViewBag.Error);
		}

		// -------------------------------
		// VERIFY EMAIL TESTS
		// -------------------------------

		[TestMethod]
		public async Task VerifyEmail_ValidToken_ReturnsSuccessMessage()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var user = fakeRepo.Users.First(u => u.email == "test@test.com");

			var result = await controller.VerifyEmail(user.VerificationToken) as ContentResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Email verified successfully! You can now log in.", result.Content);
		}

		[TestMethod]
		public async Task VerifyEmail_ExpiredToken_ReturnsExpiredMessage()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var user = fakeRepo.Users.First(u => u.email == "expired@test.com");

			var result = await controller.VerifyEmail(user.VerificationToken) as ContentResult;

			Assert.IsNotNull(result);
			Assert.AreEqual("Verification link has expired.", result.Content);
		}

		// -------------------------------
		// INDEX TEST
		// -------------------------------

		[TestMethod]
		public async Task Index_ReturnsViewWithUsers()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var result = await controller.Index() as ViewResult;

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Model);
		}

		// -------------------------------
		// DETAILS TEST
		// -------------------------------

		[TestMethod]
		public async Task Details_ValidId_ReturnsView()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var result = await controller.Details(1) as ViewResult;

			Assert.IsNotNull(result);
		}

		[TestMethod]
		public async Task Details_InvalidId_ReturnsNotFound()
		{
			var fakeRepo = new FakeUserRepository();
			var controller = GetController(fakeRepo);

			var result = await controller.Details(999);

			Assert.IsInstanceOfType(result, typeof(HttpStatusCodeResult));
		}
	}
}

