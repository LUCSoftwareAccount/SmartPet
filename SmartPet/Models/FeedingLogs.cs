using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class FeedingLogs
    {
		public int FeedingLogId { get; set; }  // Primary Key
		public DateTime FeedingTime { get; set; } // Date and time when the pet was fed
		public string FoodType { get; set; } // Type of food (e.g., Dry Food, Wet Food)
		public double Amount { get; set; } // Amount of food fed (e.g., in grams or cups)
		public string Notes { get; set; } // Optional notes (e.g., Special Instructions)

		// Foreign key to Pet
		public int PetId { get; set; }

	}
}