using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Activity
    {
		public int id { get; set; } // Primary Key
		public bool completed { get; set; } // Wether or not activity is completed
		public DateTime activityDate { get; set; } // Date and time of the activity
		public int petId { get; set; } // Foreign key to Pet
		public DateTime createdAt { get; set; } // When activity was created
		public string activityName { get; set; } = string.Empty;

		[ForeignKey("petId")]
		public virtual Pet Pet { get; set; }
	}
}