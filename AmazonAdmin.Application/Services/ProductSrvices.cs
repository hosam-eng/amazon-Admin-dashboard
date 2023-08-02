using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonAdmin.Application.Services
{
    public class ProductSrvices : IProductServices
    {
        private readonly IProductReposatory _reposatory;
        private readonly IMapper _mapper;
        private readonly IImageReposatory _Imgrepo;
        private readonly IImageService imageService;
		private readonly IWebHostEnvironment _webHostEnvironment;
		public ProductSrvices(IProductReposatory reposatory,IWebHostEnvironment webHostEnvironment ,IMapper mapper,IImageReposatory imageReposatory, IImageService _imageService)
        {
            _reposatory = reposatory;
            _mapper = mapper;
            _Imgrepo= imageReposatory;
            imageService = _imageService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<List<ShowProductDTO>> FilterByPrice(int catid,decimal initprice, decimal finalprice)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList = res.Where(p => p.Price >= initprice && p.Price <= finalprice && p.CategoryId==catid);
            return _mapper.Map<List<ShowProductDTO>>(FillterdList);
        }

        public async Task<List<ShowProductDTO>> GetAllProducts()
        {
            var products = await _reposatory.GetAllAsync();
            var res = products.Where(p => p.Status == true);
            return _mapper.Map<List<ShowProductDTO>>(res);
        }
        public async Task<IQueryable<Product>> GetAllProductsQuarable(string valueSearch)
        {
            var products = await _reposatory.GetAllAsync();
            var res = products.Where(p => string.IsNullOrEmpty(valueSearch) ? true :
            p.Name.Contains(valueSearch) || p.arabicName.Contains(valueSearch));
            return res;
        }
        public async Task<List<ShowProductDTO>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _reposatory.GetAllAsync();
            var filtterdproducts = products.
                Where(p => p.CategoryId == categoryId && p.UnitInStock > 0);
            var ProductsList= _mapper.Map<List<ShowProductDTO>>(filtterdproducts);
            foreach (var item in ProductsList)
            {
                item.images = await _Imgrepo.GetImagesByPrdId(item.Id);
            }
            return ProductsList;
        }

        public async Task<List<ShowProductDTO>> SearchByProductName(string name)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList=res.Where(p=>p.Name.ToLower().Contains(name.ToLower()));
            return _mapper.Map<List<ShowProductDTO>>(FillterdList);
        }
        #region User Services

        public async Task<List<ShowProductDTO>> ShowProductsPagination(int pagenumber, int items)
        {
            var products = await _reposatory.GetAllAsync();
            var paginatedList = products.Where(p => p.UnitInStock > 0)
                .Skip(items * (pagenumber - 1)).Take(items);
            return _mapper.Map<List<ShowProductDTO>>(paginatedList);
        }

        public async Task<List<ShowProductDTO>> SearchByArProductName(string Arname)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList = res.Where(p => p.arabicName.Contains(Arname));
            return _mapper.Map<List<ShowProductDTO>>(FillterdList);
        }

        public async Task<ShowProductDTO> GetProductsById(int id)
        {
            var res = await _reposatory.GetByIdAsync(id);
            var product = _mapper.Map<ShowProductDTO>(res);
            product.images = await _Imgrepo.GetImagesByPrdId(id);
            return product;
        }

        public async Task<PriceDTO> GetPriceCategoryId(int id)
        {
            var products = await _reposatory.GetAllAsync();
            var filtterdproducts = products.
                Where(p => p.CategoryId == id).ToList();
            decimal minprice = 0;
            decimal maxprice = 100;
            if (filtterdproducts.Count > 0)
            {
                minprice = filtterdproducts.Min(p => p.Price);
                maxprice = filtterdproducts.Max(p => p.Price);
            }

            return new PriceDTO() { MaxPrice = maxprice, MinPrice = minprice };
        }

        public async Task<List<ShowProductDTO>> GetProductsWithMaxPriceFillter(int catid, decimal max)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList = res.Where(p => p.Price >= max && p.CategoryId == catid);
            return _mapper.Map<List<ShowProductDTO>>(FillterdList);
        } 
        #endregion


        public async Task<ShowProductDTO> CreateProduct(AddUpdateProductDTO product)
		{
			var res=await _reposatory.CreateAsync(_mapper.Map<Product>(product));
            if (res !=null)
            {
                await _reposatory.SaveChangesAsync();
                return _mapper.Map<ShowProductDTO>(res);
            }
            else
            {
                return null;
            }
		}

		public async Task<bool> UpdateProduct(int id, AddUpdateProductDTO product, List<IFormFile> images)
		{
            Product product1 = _mapper.Map<Product>(product);
            product1.Id = id;
            var res = await _reposatory.UpdateAsync(product1);
            if (res)
            {
                await _reposatory.SaveChangesAsync();
                bool checkRes = await updateProductImages(images, id);
                if (checkRes)
                {
                    await _Imgrepo.SaveChangesAsync(); 
                }

                return true;
            }
            else 
            {
                return false; 
            }

		}

		public async Task<bool> DeleteProduct(int id)
		{
            var res = await _reposatory.DeleteAsync(id);
            if (res)
            {
                await _reposatory.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
		}

        public async Task<bool> SoftDelete(int id)
        {
            var res = await _reposatory.DeleteProductSoftly(id);
            return res ? true : false;
        }
        private async Task<bool> updateProductImages(List<IFormFile> images, int prodId)
        {
            List<int> imageIdList = imageService.getImagesIdByProductId(prodId);
            if(imageIdList.Count > 0 && images.Count > 0)
            {
                int min = Math.Min(imageIdList.Count, images.Count);
                int count = 0;
                for(int i = 0; i < min; i++)
                {
                    count=i+1;
                    string filename = "";
                    if (images[i]!= null || images[i].Length > 0)
                    {
                        filename = getFileName(images[i]);
                        await imageService.UpdateImage(new ImageDTO()
                        {
                            Id = imageIdList[i],
                            Name = filename,
                            ProductID = prodId
                        });
                    }
                }
                if(images.Count > min)
                {
                    for(int i=count; i<images.Count; i++)
                    {

                        string filename = getFileName(images[i]);
                        await imageService.uploadImage(new ImageDTO()
                        {
                            Name = filename,
                            ProductID = prodId
                        });
                    }
                }
            }
            else if (images.Count>0)
            {
                foreach(var item in images)
                {
                    string filename = getFileName(item);
                    await imageService.uploadImage(new ImageDTO()
                    {
                        Name = filename,
                        ProductID = prodId
                    });
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        private string getFileName(IFormFile image)
        {   
                string filename = "";
			//string uploads = Path.Combine(hosting.WebRootPath, "images");
                string uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploadedImages");
                filename = new Guid().ToString() + "_" + image?.FileName;
                string fullpath = Path.Combine(uploads, filename);
                image?.CopyTo(new FileStream(fullpath, FileMode.Create));

            return filename;
        }

    }
}
