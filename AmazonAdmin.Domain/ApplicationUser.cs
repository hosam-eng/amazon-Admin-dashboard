using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
    public class ApplicationUser:IdentityUser
    {
        public string? Phone { get; set; }
        public string? EmailAddress { get; set; }
        [ForeignKey("City")]
        public int? CityId { get; set; }
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Address Length Must Be Between 5 to 100 char")]
        public virtual City? City { get; set; }
        public virtual ICollection<Order> Orders { get; set; } =
            new HashSet<Order>();
    }
}
