using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
    public class shippingAddress
    {
		public int id { get; set; }
		public string street { get; set; }
		public string buildname { get; set; }
		[ForeignKey("ApplicationUser")]
		public string userid { get; set; }
		[ForeignKey("City")]
		public int? CityId { get; set; }
		public string Name { get; set; }
		public string Phone { get; set; }
		public virtual City? City { get; set; }
		public virtual ApplicationUser? ApplicationUser { get; set; }

	}
}
