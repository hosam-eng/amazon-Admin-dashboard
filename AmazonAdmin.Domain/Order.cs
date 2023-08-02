using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
	public enum Status
	{
		OrderedToday,
		Shipped,
		outForDelivery,
		Arrival
	}
	public class Order
	{
		public int Id { get; set; }
		public DateTime OrderDate { get; set; }
		public DateTime ArrivalDate { get; set; }
		public decimal total { get; set; }
        public bool IsDeleted { get; set; }
        [ForeignKey("User")]
		public string UserId { get; set; }
		public Status? status { get; set; }
		[ForeignKey("shippingAddress")]
		public int? shippingAddressId { get; set; }

		[StringLength(150, MinimumLength = 5, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
		public shippingAddress? shippingAddress { get; set; }
		public virtual ApplicationUser User { get; set; }
		public virtual ICollection<OrderItem> OrderItems { get; set; } =
			new HashSet<OrderItem>();
	}
}
