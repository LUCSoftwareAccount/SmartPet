using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
	public class Pet
	{
		public int id { get; set; }
		public int userId { get; set; }
		public string name { get; set; }
		public string type { get; set; }
		public DateTime? Birthdate { get; set; }
		public string microchipId { get; set; }
		public string photoUrl { get; set; } 
	}
} 