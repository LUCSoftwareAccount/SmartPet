using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Vaccines
    {
        public int id { get; set; }
		public int petId { get; set; }
		public string name { get; set; }
		public DateTime administeredOn { get; set; }
		public DateTime nextDueOn { get; set; }
		public string vetName { get; set; }
	}
}