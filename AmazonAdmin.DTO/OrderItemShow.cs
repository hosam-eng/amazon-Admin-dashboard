using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.DTO
{
    public class OrderItemShow
    {
        public int Id { get; set; }
        public decimal count { get; set; }
        public int OrderId { get; set; } 
        public int ProductId { get; set; }
        public string Productname { get; set; }
        public string arabicProductname { get; set; }
        public string ImgUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public decimal supTotalPrice { get; set; }
    }
}
