using System;
using System.ComponentModel.DataAnnotations;

namespace SmartPet.Models
{
	public class ReminderViewModel
	{
		[Required]
		public string Message { get; set; }

		[Required]
		public DateTime DueAt { get; set; }

		[Required]
		public int PetId { get; set; }

		public string TimeZoneId { get; set; } // The selected time zone

	}
}