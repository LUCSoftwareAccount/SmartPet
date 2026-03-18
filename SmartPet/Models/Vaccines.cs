using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class Vaccines
    {
        public int Id { get; set; }
		public int PetId { get; set; }
		public string Name { get; set; }
		public DateTime AdministeredOn { get; set; }
		public DateTime NextDueOn { get; set; }
		public string VetName { get; set; }
	}
}