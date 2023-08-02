using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string arabicProductName { get; set; }
        public decimal supTotalPrice{ get; set; }
        public string ImgUrl { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
