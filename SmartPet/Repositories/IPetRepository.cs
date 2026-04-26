using System.Collections.Generic;
using System.Threading.Tasks;
using SmartPet.Models;

namespace SmartPet.Repositories
{
	public interface IPetRepository
	{
		Task<IEnumerable<Pet>> GetAllPetsAsync();
		Task<Pet> GetPetByIdAsync(int id);
		Task AddPetAsync(Pet pet);
		Task UpdatePetAsync(Pet pet);
		Task DeletePetAsync(int id);
	}
}