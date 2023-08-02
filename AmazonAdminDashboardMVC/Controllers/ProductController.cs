using AmazonAdmin.Application.Services;
using AmazonAdmin.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
	[Authorize(Roles = "Admin")]
	public class ProductController : Controller
    {
        private readonly IProductServices _services;
        private readonly ISubcategoryServices _catservice;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IImageService _imageservice;
        private readonly IMapper _Mapper;

        public ProductController(IProductServices services, ISubcategoryServices categoryservice, IWebHostEnvironment webHostEnvironment, IImageService imageService,IMapper mapper)
        {
            _services = services;
            _catservice = categoryservice;
            _webHostEnvironment = webHostEnvironment;
            _imageservice = imageService;
            _Mapper=mapper;
        }
        public async Task<IActionResult> Index()
        {
            //return View(await _services.GetAllProducts());
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAll()
        {
            int skip = int.Parse(Request.Form["start"]);
            int pageSize = int.Parse(Request.Form["length"]);
            string searchValue = Request.Form["search[value]"];

            var products =  await _services.GetAllProductsQuarable(searchValue);

            var data = products.Skip(skip).Take(pageSize).ToList();

            var recordsTotal = products.Count();

            var jsonDate = new { recordsFiltered = recordsTotal, recordsTotal, data=products };
            return Json(jsonDate);
        }
        public async Task<IActionResult> GetProductByCatId(int id) 
        { 
			return View("Index",await _services.GetProductsByCategoryId(id));
		}
		public async Task<IActionResult> AddProduct()
        {
            List<SubCategoryDTO> categories = await _catservice.GetAllSubcategories();
            AddUpdateProductDTO createProduct = new AddUpdateProductDTO();
            createProduct.SubCategories = categories;
            return View(createProduct);
        }

		[HttpPost]
        public async Task<IActionResult> AddProduct(AddUpdateProductDTO createProductDTO,
                                          List<IFormFile> image)
        {

            if (ModelState.IsValid)
            {
                createProductDTO.Status = true;
                var result = await _services.CreateProduct(createProductDTO);
                if (result != null)
                {
                    foreach (var item in image)
                    {
						if (item == null || item.Length == 0)
						{
							createProductDTO.SubCategories = await _catservice.GetAllSubcategories();
							return View(createProductDTO);
						}
						string uploadPath = Path.GetFullPath(_webHostEnvironment.WebRootPath, "uploadedImages");
                        string filname = new Guid().ToString() + "_" + item.FileName;
                        string fullPath = Path.Combine(uploadPath, filname);

                        using (FileStream fileStream = new FileStream(fullPath, FileMode.Create))
                        {
                             item.CopyTo(fileStream);
                        }
                        await _imageservice.uploadImage(new ImageDTO() { Name = filname, ProductID = result.Id });
                    }

                    return RedirectToAction("Index");
                }
            }
            List<SubCategoryDTO> categories = await _catservice.GetAllSubcategories();
            createProductDTO.SubCategories = categories;
            return View(createProductDTO);
        }

        public async Task<IActionResult> UpdateProduct(int id)
        {
            var productModel = await _services.GetProductsById(id);
            AddUpdateProductDTO productDTO = _Mapper.Map<AddUpdateProductDTO>(productModel);
            List<SubCategoryDTO> categories = await _catservice.GetAllSubcategories();
            productDTO.imageDTOs =  _imageservice.gitImagesByProdId(id);
            productDTO.SubCategories = categories;
            return View(productDTO);
        }


		[HttpPost]
		public async Task<IActionResult> UpdateProduct(AddUpdateProductDTO createProductDTO,int id, List<IFormFile> image)
		{
			if (ModelState.IsValid)
			{
				var result = await _services.UpdateProduct(id,createProductDTO,image);
				if (result)
                {
                    return RedirectToAction("Index");
                }
               
			}
			createProductDTO.SubCategories = await _catservice.GetAllSubcategories();
			return View(createProductDTO);
		}
		public async Task<IActionResult> DeleteProduct(int id)
		{
			var res=await _services.SoftDelete(id);
                return RedirectToAction("Index");
		}





	}
}
