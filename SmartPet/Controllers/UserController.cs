using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartPet.Models;
using SmartPet.Repositories;

namespace SmartPet.Controllers
{
	public class UserController : Controller
	{
		private readonly IUserRepository _userRepository;

		public UserController()
		{
			_userRepository = new UserRepository();
		}

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		// GET: User
		public async Task<ActionResult> Index()
		{
			var users = await _userRepository.GetAllUsersAsync();
			return View(users);
		}

		// GET: User/Details/5
		public async Task<ActionResult> Details(int id)
		{
			var user = await _userRepository.GetUserByIdAsync(id);

			if (user == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(user);
		}

		// GET: User/Registration
		public ActionResult Registration()
		{
			return View();
		}

		// POST: User/Registration
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Registration([Bind(Include = "username,email,passwordHash")] User user)
		{
			if (ModelState.IsValid)
			{
				user.enabled = true;
				user.isVerified = false;

				await _userRepository.AddUserAsync(user);
				return RedirectToAction("Login");
			}

			return View(user);
		}

		// GET: User/Edit/5
		public async Task<ActionResult> Edit(int id)
		{
			var user = await _userRepository.GetUserByIdAsync(id);

			if (user == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(user);
		}

		// POST: User/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit(int id, [Bind(Include = "id,username,email,passwordHash,enabled,isVerified")] User user)
		{
			if (id != user.id)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			if (ModelState.IsValid)
			{
				await _userRepository.UpdateUserAsync(user);
				return RedirectToAction("Index");
			}

			return View(user);
		}

		// GET: User/Delete/5
		public async Task<ActionResult> Delete(int id)
		{
			var user = await _userRepository.GetUserByIdAsync(id);

			if (user == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.NotFound);
			}

			return View(user);
		}

		// POST: User/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			await _userRepository.DeleteUserAsync(id);
			return RedirectToAction("Index");
		}

		// GET: User/Login
		public ActionResult Login()
		{
			return View();
		}

		// POST: User/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Login(string Email, string Password)
		{
			if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
			{
				ViewBag.Error = "Please enter both email and password.";
				return View();
			}

			// temporary test login
			if (Email == "test@test.com" && Password == "1234")
			{
				return RedirectToAction("Index");
			}

			ViewBag.Error = "Invalid email or password.";
			return View();
		}
	}
}