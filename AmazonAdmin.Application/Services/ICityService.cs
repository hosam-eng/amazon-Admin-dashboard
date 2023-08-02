using AmazonAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public interface ICityService
    {
        Task<List<CitiesListDTO>> GetCities();
        Task<bool> AddCity(CreateUpdateCity citiesListDTO);
        Task<bool> UpdateCity(int id, CreateUpdateCity citiesListDTO);
        Task<bool> DeleteCity(int id);
        Task<List<CitiesListDTO>> GetCitiesByCountry(int id);
        Task<CreateUpdateCity> GetCityById(int id);
    }
}
