using AmazonAdmin.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmazonAdmin.DTO
{
    public class AddCategoryDto
    {
        public int Id { get; set; }
        [MinLength(3, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
        public string Name { get; set; }
        [MinLength(3, ErrorMessage = "يجب ان لا يقل الاسم عن 3 حروف")]
        [Display(Name = "Arabic Name")]
        public string arabicName { get; set; }
        public int? categoryId { get; set; }
        public List<CategoryDTO>? Categories { get; set; }
        public string? ImageUrl { get; set; }
        [Display(Name = "Image")]
        [AllowedImageExtensions(".jpg", ".png", ".gif")]
        //[RegularExpression(@"^.*\.(jpg|png|gif)", ErrorMessage = "Please Select Extention for image only jpg or png")]
        public IFormFile? imageFile { get; set; }
    }
}
