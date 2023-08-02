using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Context;
using AmazonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Infrastructure
{
	public class SubCategoryReposatory : Reposatory<Category, int>, ISubCategoryReposatory
	{
		public SubCategoryReposatory(ApplicationContext context) : base(context)
		{
		}
	}
}
