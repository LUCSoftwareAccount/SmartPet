using System;
using System.Collections.Generic;

namespace SmartPet.Models
{
	public class DashboardViewModel
	{
		public int TotalPets { get; set; }
		public int TotalReminders { get; set; }
		public int TotalVaccines { get; set; }
		public int TotalNotes { get; set; }
		public int TotalMedicationLogs { get; set; }

		public List<Pet> Pets { get; set; }
		public List<Reminder> UpcomingReminders { get; set; }
		public List<Vaccines> RecentVaccines { get; set; }
		public List<Note> RecentNotes { get; set; }
		public List<DashboardMedicationLogItem> RecentMedicationLogs { get; set; }

		public DashboardViewModel()
		{
			Pets = new List<Pet>();
			UpcomingReminders = new List<Reminder>();
			RecentVaccines = new List<Vaccines>();
			RecentNotes = new List<Note>();
			RecentMedicationLogs = new List<DashboardMedicationLogItem>();
		}
	}

	public class DashboardMedicationLogItem
	{
		public int LogId { get; set; }
		public string MedicationName { get; set; }
		public string Amount { get; set; }
		public DateTime AdministeredAt { get; set; }
		public int PetId { get; set; }
	}
}