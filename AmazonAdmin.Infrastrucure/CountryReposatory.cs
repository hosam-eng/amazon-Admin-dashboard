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
    public class CountryReposatory : Reposatory<Country, int>, IcountryReposatory
    {
        public CountryReposatory(ApplicationContext context) : base(context)
        {
        }
    }
}
