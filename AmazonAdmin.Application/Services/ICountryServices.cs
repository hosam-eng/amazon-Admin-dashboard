using AmazonAdmin.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Services
{
    public interface ICountryServices
    {
        Task<List<CountryDTO>> GetAll();
        Task<CountryDTO> GetById(int id);
        Task<bool> AddCountry(CountryDTO country);
        Task<bool> UpdateCountry(CountryDTO country,int id);
    }
}
