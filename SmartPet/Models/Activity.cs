using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Activity
    {
		public int ActivityId { get; set; } // Primary Key
		public string ActivityType { get; set; } // Type of activity (e.g., Walk, Play, Feed)
		public DateTime ActivityDate { get; set; } // Date and time of the activity
		public int PetId { get; set; } // Foreign key to Pet
	}
}