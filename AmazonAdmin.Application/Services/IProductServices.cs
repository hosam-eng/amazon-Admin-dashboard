using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public interface IProductServices
    {
        Task<List<ShowProductDTO>> ShowProductsPagination(int pagenumber,int items);
        Task<List<ShowProductDTO>> GetProductsByCategoryId(int categoryId);
        Task<ShowProductDTO> GetProductsById(int id);
        Task<List<ShowProductDTO>> GetAllProducts();
        Task<IQueryable<Product>> GetAllProductsQuarable(string valueSearch);
        Task<List<ShowProductDTO>> FilterByPrice(int catid,decimal initprice,decimal finalprice);
        Task<List<ShowProductDTO>> SearchByProductName(string name);
        Task<List<ShowProductDTO>> SearchByArProductName(string Arname);
        Task<PriceDTO> GetPriceCategoryId(int id);
        Task<List<ShowProductDTO>> GetProductsWithMaxPriceFillter(int catid, decimal max);
        Task<ShowProductDTO> CreateProduct(AddUpdateProductDTO product);
        Task<bool> UpdateProduct(int id, AddUpdateProductDTO product,List<IFormFile> images);
        Task<bool> DeleteProduct(int id);
        Task<bool> SoftDelete(int id);
    }
}
