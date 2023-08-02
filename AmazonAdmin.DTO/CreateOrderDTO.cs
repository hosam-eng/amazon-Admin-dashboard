using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AmazonAdmin.Domain;

namespace AmazonAdmin.DTO
{
    public class CreateOrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string UserId { get; set; }
        public Status status { get; set; }
        public int shippingAddressId { get; set; }

        [StringLength(150, MinimumLength = 5, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
        public shippingAddress? shippingAddress { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
