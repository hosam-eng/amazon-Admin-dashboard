using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
    public class City
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 3,ErrorMessage ="Name Length Must Be Between 3 to 50 char")]
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual ICollection<ApplicationUser> Users { get; set; } =
            new HashSet<ApplicationUser>();
        public virtual Country Country { get; set; }
    }
}
