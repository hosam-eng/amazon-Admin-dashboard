using AmazonAdmin.Application.Contracts;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public class CityService:ICityService
    {
        private readonly ICityReposatory _cityRepo;
        private readonly IMapper _mapper;

        public CityService(ICityReposatory cityRepo,IMapper mapper)
        {
            _cityRepo = cityRepo;
            _mapper = mapper;
        }

        public async Task<bool> AddCity(CreateUpdateCity citiesListDTO)
        {
            var res=await _cityRepo.CreateAsync(_mapper.Map<City>(citiesListDTO));
            if(res != null)
            {
                await _cityRepo.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteCity(int id)
        {
            var res = await _cityRepo.DeleteAsync(id);
            if (res)
            {
                await _cityRepo.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<CitiesListDTO>> GetCities()
        {
            return _mapper.Map<List<CitiesListDTO>>(await _cityRepo.GetAllAsync());
        }

        public async Task<List<CitiesListDTO>> GetCitiesByCountry(int id)
        {
            return _mapper.Map<List<CitiesListDTO>>(await _cityRepo.GetCitiesbyCountry(id));
        }

        public async Task<CreateUpdateCity> GetCityById(int id)
        {
            return _mapper.Map<CreateUpdateCity>(await _cityRepo.GetByIdAsync(id));
        }

        public async Task<bool> UpdateCity(int id, CreateUpdateCity citiesListDTO)
        {
            City city = _mapper.Map<City>(citiesListDTO);
            city.Id = id;
            var res = await _cityRepo.UpdateAsync(city);
            if (res)
            {
                await _cityRepo.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
