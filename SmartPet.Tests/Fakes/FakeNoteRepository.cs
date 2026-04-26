using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartPet.Models;
using SmartPet.Repositories;

namespace SmartPet.Tests.Fakes
{
	public class FakeNoteRepository : INoteRepository
	{
		public List<Note> Notes { get; set; } = new List<Note>();

		public FakeNoteRepository()
		{
			Notes.Add(new Note
			{
				id = 1,
				petId = 1,
				content = "First note"
			});

			Notes.Add(new Note
			{
				id = 2,
				petId = 2,
				content = "Second note"
			});
		}

		public Task<IEnumerable<Note>> GetAllNotesAsync()
		{
			return Task.FromResult(Notes.AsEnumerable());
		}

		public Task<IEnumerable<Note>> GetNotesByPetIdAsync(int petId)
		{
			return Task.FromResult(Notes.Where(n => n.petId == petId).AsEnumerable());
		}

		public Task<Note> GetNoteByIdAsync(int id)
		{
			return Task.FromResult(Notes.FirstOrDefault(n => n.id == id));
		}

		public Task AddNoteAsync(Note note)
		{
			Notes.Add(note);
			return Task.CompletedTask;
		}

		public Task UpdateNoteAsync(Note note)
		{
			var existing = Notes.FirstOrDefault(n => n.id == note.id);

			if (existing != null)
			{
				existing.petId = note.petId;
				existing.content = note.content;
				existing.createdAt = note.createdAt;
				existing.updateAt = note.updateAt;
			}

			return Task.CompletedTask;
		}

		public Task DeleteNoteAsync(int id)
		{
			var note = Notes.FirstOrDefault(n => n.id == id);

			if (note != null)
			{
				Notes.Remove(note);
			}

			return Task.CompletedTask;
		}
	}
}

