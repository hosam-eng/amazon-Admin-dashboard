using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonAdmin.Domain
{
    public class Category
    {
        public int Id { get; set; }
        [StringLength(50,MinimumLength = 3,ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
        public string Name { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
        public string arabicName { get; set; }

        [StringLength(200,MinimumLength =10, ErrorMessage = "Name Length Must Be Between 10 to 200 char")]
        public virtual ICollection<Product> Products { get; set; } =
            new HashSet<Product>();
        public virtual ICollection<Category>? Categories { get; set; } =
            new HashSet<Category>();
        public virtual Image? image { get; set; }

        [ForeignKey("category")]
        public int? categoryId { get; set; }
        public virtual Category? category { get; set; }
    }
}