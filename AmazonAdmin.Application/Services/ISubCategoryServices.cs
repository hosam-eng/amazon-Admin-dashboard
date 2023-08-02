using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
	public interface ISubcategoryServices
	{
		Task<List<SubCategoryDTO>> getSubCategoryByCatId(int id);
		Task<List<SubCategoryDTO>> GetAllSubcategories();
        Task<IQueryable<Category>> GetAllSubCategoryQuarable(int catId,string searchValue);


    }
}
