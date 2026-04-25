using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Medication
    {
		public int id { get; set; }
		public int petId { get; set; }
		public string name { get; set; }
		public string dosage { get; set; }
		public DateTime? startDate { get; set; }
		public string notes { get; set; }
		
	}
}