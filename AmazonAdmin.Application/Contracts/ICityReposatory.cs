using AmazonAdmin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Contracts
{
    public interface ICityReposatory:IReposatory<City,int>
    {
		Task<List<City>> GetCitiesbyCountry(int id);
	}
}
