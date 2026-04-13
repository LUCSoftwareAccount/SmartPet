using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Note
    {
		public int id { get; set; }
		public int petId { get; set; }
		public string content { get; set; }
		public DateTime createdAt { get; set; }
		public DateTime? updateAt { get; set; } 
		


	}
}