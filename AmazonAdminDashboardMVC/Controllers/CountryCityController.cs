using AmazonAdmin.Application.Services;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using Microsoft.AspNetCore.Mvc;

namespace AmazonAdminDashboardMVC.Controllers
{
    public class CountryCityController : Controller
    {
        private readonly ICountryServices _countryservices;
        private readonly ICityService _cityService;
        public CountryCityController(ICountryServices countryservices,ICityService cityService)
        {
            _countryservices = countryservices;
            _cityService = cityService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _countryservices.GetAll());
        }
        public async Task<IActionResult> AddCountry()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCountry(CountryDTO country)
        {
            if (ModelState.IsValid)
            {
                var res = await _countryservices.AddCountry(country);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed To Add Country");
                }
            }
            return View(country);
        }
        public async Task<IActionResult> UpdateCountry(int id)
        {
            return View(await _countryservices.GetById(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCountry(int id, CountryDTO country)
        {
            if (ModelState.IsValid)
            {
                var res = await _countryservices.UpdateCountry(country, id);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed To Update Country");
                }
            }
            return View(country);
        }

        public async Task<IActionResult> GetCountryCities(int id)
        {
            return View(await _cityService.GetCitiesByCountry(id));
        }

        public async Task<IActionResult> AddCity()
        {
            CreateUpdateCity city = new CreateUpdateCity();
            city.Countries = await _countryservices.GetAll();
            return View(city);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(CreateUpdateCity city)
        {
            if (ModelState.IsValid)
            {
                var res = await _cityService.AddCity(city);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed To Add City");
                }
            }
            city.Countries = await _countryservices.GetAll();
            return View(city);
        }

        public async Task<IActionResult> UpdateCity(int id)
        {
            var city = await _cityService.GetCityById(id);
            city.Countries = await _countryservices.GetAll();
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCity(CreateUpdateCity city, int id)
        {
            if (ModelState.IsValid)
            {
                var res = await _cityService.UpdateCity(id,city);
                if (res)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Failed To Update City");
                }
            }
            city.Countries = await _countryservices.GetAll();
            return View(city);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var res = await _cityService.DeleteCity(id);
            return RedirectToAction("Index");
        }
    }
}
