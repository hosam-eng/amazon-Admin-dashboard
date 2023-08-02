using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
    public class Product
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Name Length Must Be More Than 3 Char")]
        public String Name { get; set; }

        [MinLength(3, ErrorMessage = "Name Length Must Be More Than 3 Char")]
        public String arabicName { get; set; }
        public decimal Price { get; set; }
        public int UnitInStock { get; set; }
        public bool Status { get; set; }
        [MinLength(3, ErrorMessage = "Name Length Must Be More Than 3 Char")]
        public string Description { get; set; }

        [MinLength(3, ErrorMessage = "Name Length Must Be More Than 3 Char")]
        public string arabicDescription { get; set; }
        public virtual ICollection<Image> Images { get; set; } =
            new HashSet<Image>();
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } =
            new HashSet<OrderItem>();
    }
}