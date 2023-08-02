using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Domain
{
	public enum Star
	{
		oneStar = 1,
		twoStar,
		threeStar,
		fourStar,
		fiveStar,
	}
	public class Rating
	{
		public int id { get; set; }
		public Star rate { get; set; }
		public string review { get; set; }
		public string userName { get; set; }

		[ForeignKey("Product")]
		public int productId { get; set; }
		public Product Product { get; set; }
	}
}