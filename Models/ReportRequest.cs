using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class ReportRequest
    {
		public int id { get; set; }
		public int petId { get; set; }
		public string reportType { get; set; }
		public string status { get; set; }
		public string generatedUrl { get; set; }
	}
}