using AmazonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AmazonAdmin.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string UserId { get; set; }
        public List<OrderItemShow>? OrderItems { get; set; }
        public string? UserName { get; set; }
        public Status? status { get; set; }
    }
}
