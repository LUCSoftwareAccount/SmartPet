using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Activity
    {
		public int id { get; set; } // Primary Key
		public bool completed { get; set; } // Type of activity (e.g., Walk, Play, Feed)
		public DateTime activityDate { get; set; } // Date and time of the activity
		public int petId { get; set; } // Foreign key to Pet
		public DateTime createdAt { get; set; } // When activity was created
	}
}