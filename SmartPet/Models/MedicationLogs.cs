using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class MedicationLogs
    {
		public int MedicationLogId { get; set; }  // Primary Key
		public string MedicationName { get; set; } // Name of the medication
		public string Dosage { get; set; } // Dosage of the medication (e.g., "10 mg")
		public DateTime AdministeredAt { get; set; } // Date and time when the medication was given
		public string Notes { get; set; } // Optional notes (e.g., Special Instructions, Side Effects)

		// Foreign key to Pet
		public int PetId { get; set; }

	}
}