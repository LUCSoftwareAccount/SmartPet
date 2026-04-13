using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SmartPet.Models;

namespace SmartPet.Repositories
{
	public class NoteRepository : INoteRepository
	{
		private readonly SmartPetDbContext _context;

		public NoteRepository(SmartPetDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Note>> GetAllNotesAsync()
		{
			return await _context.Notes.ToListAsync();
		}

		public async Task<IEnumerable<Note>> GetNotesByPetIdAsync(int petId)
		{
			return await _context.Notes
				.Where(n => n.petId == petId)
				.OrderByDescending(n => n.createdAt)
				.ToListAsync();
		}

		public async Task<Note> GetNoteByIdAsync(int id)
		{
			return await _context.Notes.FindAsync(id);
		}

		public async Task AddNoteAsync(Note note)
		{
			note.createdAt = System.DateTime.Now;
			_context.Notes.Add(note);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateNoteAsync(Note note)
		{
			_context.Entry(note).State = EntityState.Modified;
			note.updateAt = System.DateTime.Now;
			await _context.SaveChangesAsync();
		}

		public async Task DeleteNoteAsync(int id)
		{
			var note = await _context.Notes.FindAsync(id);
			if (note != null)
			{
				_context.Notes.Remove(note);
				await _context.SaveChangesAsync();
			}
		}
	}
}