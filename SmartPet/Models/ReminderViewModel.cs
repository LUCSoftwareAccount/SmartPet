using System;

namespace SmartPet.Models
{
	public class ReminderViewModel
	{
		public int PetId { get; set; }
		public string Message { get; set; }
		public DateTime DueAt { get; set; }
	}
}