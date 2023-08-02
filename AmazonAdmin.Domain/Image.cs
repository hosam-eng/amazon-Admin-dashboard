using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
    public class Image
    {
        public int Id { get; set; }
        [RegularExpression(@"\w+\.(jpg|png|gif)")]
        public string Name { get; set; }
        [ForeignKey("Product")]
        public int? ProductID { get; set; }
        public virtual Product? Product { get; set; }

        [ForeignKey("Category")]
        public int? categoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
