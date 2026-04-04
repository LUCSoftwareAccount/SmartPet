using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartPet.Models;
using SmartPet.Repositories;

namespace SmartPet.Controllers { 
	public class UserController : Controller
{
	private readonly IUserRepository _userRepository;

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

	// GET: User/Create
	public ActionResult Create()
	{
		return View();
	}

	// POST: User/Create
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<ActionResult> Create([Bind(Include = "FirstName, LastName, Email")] User user)
	{
		if (ModelState.IsValid)
		{
			await _userRepository.AddUserAsync(user);
			return RedirectToAction(nameof(Index));
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
	public async Task<ActionResult> Edit(int id, [Bind(Include = "id, FirstName, LastName, Email")] User user)
	{
		if (id != user.id)
		{
			return new HttpStatusCodeResult(HttpStatusCode.NotFound);
		}

		if (ModelState.IsValid)
		{
			await _userRepository.UpdateUserAsync(user);
			return RedirectToAction(nameof(Index));
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
		return RedirectToAction(nameof(Index));
	}
  }
}