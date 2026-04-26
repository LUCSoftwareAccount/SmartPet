using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Reminder
    {
		public int id { get; set; }
		public int petId { get; set; }
		public string reminderType { get; set; }
		public DateTime dueAt { get; set; }
		public string message { get; set; }

	}
}