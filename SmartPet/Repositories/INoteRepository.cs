using System.Collections.Generic;
using System.Threading.Tasks;
using SmartPet.Models;

namespace SmartPet.Repositories
{
	public interface INoteRepository
	{
		Task<IEnumerable<Note>> GetAllNotesAsync();
		Task<IEnumerable<Note>> GetNotesByPetIdAsync(int petId);
		Task<Note> GetNoteByIdAsync(int id);
		Task AddNoteAsync(Note note);
		Task UpdateNoteAsync(Note note);
		Task DeleteNoteAsync(int id);
	}
}