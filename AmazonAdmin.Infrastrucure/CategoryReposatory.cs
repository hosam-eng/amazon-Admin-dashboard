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
	public class CategoryReposatory : Reposatory<Category, int>, ICategoryReposatory
	{
		public CategoryReposatory(ApplicationContext context) : base(context)
		{
		}
	}
}
