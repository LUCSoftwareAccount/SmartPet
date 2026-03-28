using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
	public class Pet
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public DateTime? Birthdate { get; set; }
		public string MicrochipId { get; set; }
		public string PhotoUrl { get; set; }
	}
}