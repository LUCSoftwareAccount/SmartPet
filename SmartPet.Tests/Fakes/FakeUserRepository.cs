using SmartPet.Models;
using SmartPet.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartPet.Tests.Fakes
{
	public class FakeUserRepository : IUserRepository
	{
		public List<User> Users { get; set; }

		public FakeUserRepository()
		{
			Users = new List<User>
			{
				new User
				{
					id = 1,
					username = "testuser",
					email = "test@test.com",
					passwordHash = "1234",
					enabled = true,
					isVerified = true,
					VerificationToken = "valid123",
					TokenExpires = DateTime.UtcNow.AddHours(1)
				},
				new User
				{
					id = 2,
					username = "unverified",
					email = "unverified@test.com",
					passwordHash = "1234",
					enabled = true,
					isVerified = false,
					VerificationToken = "unverified123",
					TokenExpires = DateTime.UtcNow.AddHours(1)
				},
				new User
				{
					id = 3,
					username = "expired",
					email = "expired@test.com",
					passwordHash = "1234",
					enabled = true,
					isVerified = false,
					VerificationToken = "expired123",
					TokenExpires = DateTime.UtcNow.AddHours(-1)
				}
			};
		}

		public Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return Task.FromResult(Users.AsEnumerable());
		}

		public Task<User> GetUserByIdAsync(int id)
		{
			return Task.FromResult(Users.FirstOrDefault(u => u.id == id));
		}

		public Task AddUserAsync(User user)
		{
			user.id = Users.Count + 1;
			Users.Add(user);
			return Task.CompletedTask;
		}

		public Task UpdateUserAsync(User user)
		{
			var existing = Users.FirstOrDefault(u => u.id == user.id);
			if (existing != null)
			{
				existing.username = user.username;
				existing.email = user.email;
				existing.passwordHash = user.passwordHash;
				existing.enabled = user.enabled;
				existing.isVerified = user.isVerified;
				existing.VerificationToken = user.VerificationToken;
				existing.TokenExpires = user.TokenExpires;
			}

			return Task.CompletedTask;
		}

		public Task DeleteUserAsync(int id)
		{
			var user = Users.FirstOrDefault(u => u.id == id);
			if (user != null)
			{
				Users.Remove(user);
			}

			return Task.CompletedTask;
		}
	}
}
