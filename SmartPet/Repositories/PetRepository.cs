using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using SmartPet.Models;

namespace SmartPet.Repositories
{
	public class PetRepository : IPetRepository
	{
		private readonly SmartPetDbContext _context;

		public PetRepository(SmartPetDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Pet>> GetAllPetsAsync()
		{
			return await _context.Pets.ToListAsync();
		}

		public async Task<Pet> GetPetByIdAsync(int id)
		{
			return await _context.Pets.FindAsync(id);
		}

		public async Task AddPetAsync(Pet pet)
		{
			_context.Pets.Add(pet);
			await _context.SaveChangesAsync();
		}

		public async Task UpdatePetAsync(Pet pet)
		{
			_context.Entry(pet).State = EntityState.Modified;
			await _context.SaveChangesAsync();
		}

		public async Task DeletePetAsync(int id)
		{
			var pet = await _context.Pets.FindAsync(id);
			if (pet != null)
			{
				_context.Pets.Remove(pet);
				await _context.SaveChangesAsync();
			}
		}
	}
}