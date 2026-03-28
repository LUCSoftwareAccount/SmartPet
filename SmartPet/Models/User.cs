using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartPet.Models
{
    public class User
    {
			public int id { get; set; }
			public string username { get; set; }
			public string email { get; set; }
			public string passwordHash { get; set; }
			public bool enabled { get; set; }
			public bool isVerified { get; set; }
		
	}
}
