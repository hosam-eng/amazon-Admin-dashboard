using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AmazonAdmin.DTO
{
    public class RatingDTO
    {
        public int rate { get; set; }
        public string review { get; set; }
        public string userName { get; set; }
        public int productId { get; set; }
    }
}
