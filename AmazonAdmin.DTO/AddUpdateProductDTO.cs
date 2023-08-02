using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.DTO
{
    public class AddUpdateProductDTO
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Name Length Must Be More Than 3 Char")]
        public String Name { get; set; }
        [MinLength(5, ErrorMessage = "يجب ان لا يقل الاسم عن 5 حروف")]
        public String arabicName { get; set; }
        public bool Status { get; set; }
        public decimal Price { get; set; }
        public int UnitInStock { get; set; }
        [MinLength(3, ErrorMessage = "Name Length Must Be More Than 3 Char")]
        public string Description { get; set; }
        [MinLength(5, ErrorMessage = "يجب ان لا يقل الاسم عن 5 حروف")]
        public string arabicDescription { get; set; }
        public int CategoryId { get; set; }
        public List<SubCategoryDTO>? SubCategories { get; set; }
        public List<ImageDTO>? imageDTOs { get; set; }
    }
}
