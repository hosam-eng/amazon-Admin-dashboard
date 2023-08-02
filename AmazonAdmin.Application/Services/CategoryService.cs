using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AmazonAdmin.Application.Services
{
    public class CategoryService : IcategoryServices
    {
        private readonly ICategoryReposatory _Repo;
		private readonly IImageReposatory _ImageRepo;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment hosting;
        private readonly IImageService imageService;
        public CategoryService(ICategoryReposatory repo,
            IMapper mapper, IWebHostEnvironment _hosting,
			IImageReposatory ImageRepo,
			IImageService _imageService
           )
        {
            _Repo = repo;
            this.mapper = mapper;
            hosting = _hosting;
            _ImageRepo = ImageRepo;
            imageService = _imageService;
        }

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var categories = (await _Repo.GetAllAsync())
                .Where(c => c.categoryId == null).ToList();
            return mapper.Map<List<CategoryDTO>>(categories);
        }
		public async Task<IQueryable<Category>> GetAllCategoryQuarable(string searchValue)
		{
			var categories = (await _Repo.GetAllAsync())
			   .Where(c => c.categoryId == null && (string.IsNullOrEmpty(searchValue) ? true :
               (c.Name.Contains(searchValue)||c.arabicName.Contains(searchValue))));
			return categories;
		}

		public async Task<CategoryDTO> GetByIdAsync(int ID)
        {
            var category = await _Repo.GetByIdAsync(ID);
            return mapper.Map<CategoryDTO>(category);
        }


        public async Task<AddCategoryDto> CreateAsync(AddCategoryDto categoryVm)
        {
            var category = new Category()
            {
                Name = categoryVm.Name,
                arabicName = categoryVm.arabicName,
                categoryId = (categoryVm.categoryId != 0) ? categoryVm.categoryId : null,
            };

            var Categoryres = await _Repo.CreateAsync(category);
            await _Repo.SaveChangesAsync();

			if (Categoryres != null)
			{
				bool validateInsert = await generateImageName(categoryVm.imageFile, Categoryres.Id);
                await _ImageRepo.SaveChangesAsync();
			}
			return mapper.Map<AddCategoryDto>(Categoryres);
        }

        public async Task<AddCategoryDto> UpdateAsync(AddCategoryDto categoryVm, int id)
        {
            var category = new Category()
            {
                Id = id,
                Name = categoryVm.Name,
                arabicName = categoryVm.arabicName,
                categoryId = (categoryVm.categoryId != 0) ? categoryVm.categoryId : null,
            };
            var res = await _Repo.UpdateAsync(category);
            
            bool ValidInsert = await generateImageName(categoryVm.imageFile,id);
         
		    await _Repo.SaveChangesAsync();
			
			return mapper.Map<AddCategoryDto>(category);
        }
        private async Task<bool> generateImageName(IFormFile? image,int catId)
        {
            string filename = "";
            if (image != null || image?.Length > 0)
            {
				string uploads = Path.Combine(hosting.WebRootPath, "uploadedImages");
				filename = new Guid().ToString() + "_" + image?.FileName;
                string fullpath = Path.Combine(uploads, filename);
                image?.CopyTo(new FileStream(fullpath, FileMode.Create));

				var imageId = imageService.getImageObjByCategoryId(catId);
                if (imageId != 0)
                {
					await imageService.UpdateImage(new ImageDTO()
					{
						Id = imageId,
						Name = filename,
						categoryId = catId
					});
				}
                else
                {
					await imageService.uploadImage(new ImageDTO()
					{
						Name = filename,
						categoryId = catId
					});
				}
                return true;
			}

            return false;
        }

		
	}
}
