using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class ReportRequest
    {
		public int Id { get; set; }
		public int PetId { get; set; }
		public string ReportType { get; set; }
		public string Status { get; set; }
		public string GeneratedUrl { get; set; }
	}
}