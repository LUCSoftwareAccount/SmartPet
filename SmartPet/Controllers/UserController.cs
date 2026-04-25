using System;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Mvc;
using SmartPet.Models;
using SmartPet.Repositories;
using System.Diagnostics;

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
		public async Task<ActionResult> Login(FormCollection form)
		{
			string Email = form["Email"];
			string Password = form["Password"];

			var users = await _userRepository.GetAllUsersAsync();
			var user = users.FirstOrDefault(u => u.email == Email);
			Debug.WriteLine($"User found: {user?.email}");
			Debug.WriteLine($"Password found: {user?.passwordHash}");

			if (user == null || user.passwordHash != Password) { 

				ViewBag.Error = "Invalid email or password.";
			return View();
		}

		        if (!user.isVerified)
			{
				ViewBag.Error = "Please verify your email first.";
				return View();
			}

			Session["UserId"] = user.id;
			Session["UserEmail"] = user.email;

			return RedirectToAction("Dashboard", "Dashboard");
		}

		private string GenerateToken()
		{
			var bytes = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(bytes);
			}
			return Convert.ToBase64String(bytes);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Registration([Bind(Include = "username,email,passwordHash")] User user)
		{
			if (ModelState.IsValid)
			{
				user.enabled = true;
				user.isVerified = false;

				// Generate a verification token
				user.VerificationToken = GenerateToken();
				user.TokenExpires = DateTime.UtcNow.AddHours(24);

				try
				{
					// Save user to the database
					await _userRepository.AddUserAsync(user);

					// Build verification link
					var verificationLink = Url.Action(
						"VerifyEmail",
						"User",
						new { token = user.VerificationToken },
						protocol: Request.Url.Scheme
					);

					// Send verification email
					await SendVerificationEmail(user.email, verificationLink);

					// Redirect to login if email is sent successfully
					return RedirectToAction("Login");
				}
				catch (Exception ex)
				{
					// If there is an error (email not sent), remove the user from the database
					await _userRepository.DeleteUserAsync(user.id); // Assuming the user is already added with the ID

					// Log the exception for debugging (optional)
					Debug.WriteLine($"Error sending verification email: {ex}");

					// Return an error message
					ModelState.AddModelError("", "There was an error sending the verification email. Please try again.");
				}
			}

			// If model is invalid or email fails, return the registration view with error message
			return View(user);
		}

		private async Task SendVerificationEmail(string email, string link)
		{
			// Create an SMTP client
			var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587) // Using Gmail SMTP server
			{
				Credentials = new NetworkCredential("saraodish81@gmail.com", "wtxomyzyrqrmbula"), // Replace with your actual email and password
				EnableSsl = true // Enable SSL for secure connection
			};

			// Create a new mail message
			var message = new System.Net.Mail.MailMessage
			{
				From = new System.Net.Mail.MailAddress("Smartpet@gmail.com"), // Your email address (can be the same or different)
				Subject = "Verify your SmartPet account",
				Body = $"Click this link to verify your account: {link}",
				IsBodyHtml = false // The body is in plain text
			};

			// Add recipient email
			message.To.Add(email);

			try
			{
				// Send the email asynchronously
				await client.SendMailAsync(message);
			}
			catch (Exception ex)
			{
				// Log the error (optional) or throw a custom exception
				// Example: Log.Error(ex, "Error sending verification email");
				throw new Exception("Error sending verification email.", ex);
			}
		}

		public async Task<ActionResult> VerifyEmail(string token)
		{
			if (string.IsNullOrEmpty(token))
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			var users = await _userRepository.GetAllUsersAsync();
			var user = users.FirstOrDefault(u => u.VerificationToken == token);

			if (user == null)
			{
				return Content("Invalid verification link.");
			}

			if (user.TokenExpires < DateTime.UtcNow)
			{
				return Content("Verification link has expired.");
			}

			user.isVerified = true;
			user.VerificationToken = null;

			await _userRepository.UpdateUserAsync(user);

			return Content("Email verified successfully! You can now log in.");
		}
		
	}
}