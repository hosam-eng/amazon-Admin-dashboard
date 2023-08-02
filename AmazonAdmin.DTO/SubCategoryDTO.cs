using AmazonAdmin.Domain;
using System.ComponentModel.DataAnnotations;

namespace AmazonAdmin.DTO
{
	public class SubCategoryDTO
	{
		public int Id { get; set; }
		[StringLength(50, MinimumLength = 3, ErrorMessage = "Name Length Must Be Between 3 to 50 char")]
		public string Name { get; set; }
        [MinLength(5, ErrorMessage = "يجب ان لا يقل الاسم عن 5 حروف")]
        public string arabicName { get; set; }
        public int? categoryId { get; set; }

		public string? imageName { get; set; }

	}
}