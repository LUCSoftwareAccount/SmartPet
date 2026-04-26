using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class MedicationLogs
    {
		public int id { get; set; }  // Primary Key
		public int? medicationId { get; set; } // Name of the medication
		public string amount { get; set; } // Dosage of the medication (e.g., "10 mg")
		public DateTime administeredAt { get; set; } // Date and time when the medication was given
		public virtual Medication Medication { get; set; } 
		public string medicationName { get; set; } 
		public int? petId { get; set; }
		public virtual Pet Pet { get; set; }

	}
}