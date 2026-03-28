using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class FeedingLogs
    {
		public int id { get; set; }  // Primary Key
		public string brand { get; set; } // Brand of food pet was fed
		public string foodType { get; set; } // Type of food (e.g., Dry Food, Wet Food)
		public double amount { get; set; } // Amount of food fed (e.g., in grams or cups)
		public DateTime fedAt { get; set; }
		// Foreign key to Pet
		public int petId { get; set; }

	}
}