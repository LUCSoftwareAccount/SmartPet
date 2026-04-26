using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartPet.Models;
using SmartPet.Repositories;

namespace SmartPet.Tests.Fakes
{
	public class FakePetRepository : IPetRepository
	{
		public List<Pet> Pets { get; set; } = new List<Pet>();

		public FakePetRepository()
		{
			Pets.Add(new Pet
			{
				id = 1,
				userId = 11,
				name = "Milo",
				type = "Cat",
				breed = "Tabby",
				gender = "Male",
				weight = 10,
				microchipId = "12345"
			});

			Pets.Add(new Pet
			{
				id = 2,
				userId = 11,
				name = "Luna",
				type = "Dog",
				breed = "Husky",
				gender = "Female",
				weight = 25,
				microchipId = "67890"
			});
		}

		public Task<IEnumerable<Pet>> GetAllPetsAsync()
		{
			return Task.FromResult(Pets.AsEnumerable());
		}

		public Task<Pet> GetPetByIdAsync(int id)
		{
			return Task.FromResult(Pets.FirstOrDefault(p => p.id == id));
		}

		public Task AddPetAsync(Pet pet)
		{
			Pets.Add(pet);
			return Task.CompletedTask;
		}

		public Task UpdatePetAsync(Pet pet)
		{
			var existing = Pets.FirstOrDefault(p => p.id == pet.id);

			if (existing != null)
			{
				existing.userId = pet.userId;
				existing.name = pet.name;
				existing.type = pet.type;
				existing.Birthdate = pet.Birthdate;
				existing.microchipId = pet.microchipId;
				existing.photoUrl = pet.photoUrl;
				existing.breed = pet.breed;
				existing.gender = pet.gender;
				existing.weight = pet.weight;
			}

			return Task.CompletedTask;
		}

		public Task DeletePetAsync(int id)
		{
			var pet = Pets.FirstOrDefault(p => p.id == id);

			if (pet != null)
			{
				Pets.Remove(pet);
			}

			return Task.CompletedTask;
		}
	}
}

