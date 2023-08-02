using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.DTO
{
    public class AllowedImageExtensionsAttribute:ValidationAttribute
    {
        private readonly string[] _extensions;
        public AllowedImageExtensionsAttribute(params string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_extensions.Contains(extension))
                {
                    var allowedExtensions = string.Join(", ", _extensions);
                    return new ValidationResult($"Allowed file extensions are: {allowedExtensions}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
