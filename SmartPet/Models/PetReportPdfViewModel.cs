using System.Collections.Generic;

namespace SmartPet.Models
{
	public class PetReportPdfViewModel
	{
		public Pet Pet { get; set; }
		public List<MedicationLogs> MedicationLogs { get; set; }
		public List<Note> Notes { get; set; }
		public List<FeedingLogs> FeedingLogs { get; set; }
	}
}