using AmazonAdmin.Application.Services;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
    {
        private readonly IcategoryServices categoryService;
        private readonly ISubcategoryServices _subcategoryService;
        private readonly IImageService imageService;
        private readonly IMapper _Mapper;
        public CategoryController(IcategoryServices _categoryService,
            IMapper Mapper,
            IImageService _imageService,
            ISubcategoryServices subcategoryServices)
        {
            categoryService = _categoryService;
            _Mapper = Mapper;
            imageService = _imageService;
            _subcategoryService = subcategoryServices;
        }
       
        public async Task<IActionResult> Index()
        {
            //var categories = await categoryService.GetAllCategory();
            //return View(categories);
            return View();
		}
        [HttpPost]
		public async Task<IActionResult> GetAll()
		{
            int skip = int.Parse(Request.Form["start"]);
            int pageSize = int.Parse(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];

			var categories = await categoryService.GetAllCategoryQuarable(searchValue);

            var data = categories.Skip(skip).Take(pageSize).ToList();

			var recordsTotal = categories.Count();

			var jsonDate = new { recordsFiltered = recordsTotal, recordsTotal, data};
			return Json(jsonDate);
		}
		[HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await categoryService.GetAllCategory();
            AddCategoryDto categoryDto = new AddCategoryDto();
            categoryDto.Categories = categories;
            return View(categoryDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddCategoryDto categoryVm)
        {

            if (ModelState.IsValid)
            {
                var resultCategory = await categoryService.CreateAsync(categoryVm);
                if (resultCategory != null)
                {
                    return RedirectToAction("Index");
                }
            }
            var categories = await categoryService.GetAllCategory();
            categoryVm.Categories = categories;
            return View(categoryVm);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var categoryRes = await categoryService.GetByIdAsync(id);
            var categories = await categoryService.GetAllCategory();
            AddCategoryDto categoryDto = _Mapper.Map<AddCategoryDto>(categoryRes);
            categoryDto.Categories = categories;
            categoryDto.ImageUrl = await imageService.getImageByCategoryId(id);
            return View(categoryDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddCategoryDto categoryvm, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await categoryService.UpdateAsync(categoryvm, id);
                return RedirectToAction("Index");
            }
            return View();
        }
        
        public async Task<IActionResult> GetSubCategories(int id)
        {
            //List<SubCategoryDTO> subCategories = await _subcategoryService.getSubCategoryByCatId(id);
            //return View(subCategories);
            ViewBag.id = id;
            return View();

		}
    
		[HttpPost]
		public async Task<IActionResult> GetAllSubCategories(int id)
        {
            int skip = int.Parse(Request.Form["start"]);
            int pageSize = int.Parse(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];

            var categories = await _subcategoryService.GetAllSubCategoryQuarable(id,searchValue);

			var data = categories.Skip(skip).Take(pageSize).ToList();

			var recordsTotal = categories.Count();

			var jsonDate = new { recordsFiltered = recordsTotal, recordsTotal, data=categories };
			return Json(jsonDate);
		}
    }
}
