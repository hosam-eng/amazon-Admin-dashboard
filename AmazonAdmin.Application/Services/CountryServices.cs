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
    public class CountryServices : ICountryServices
    {
        private readonly IcountryReposatory _countryRepo;
        private readonly IMapper _mapper;
        public CountryServices(IcountryReposatory _countryRepo,IMapper mapper)
        {
            this._countryRepo = _countryRepo;
            this._mapper = mapper;



        }

        public async Task<bool> AddCountry(CountryDTO country)
        {
            var res = await _countryRepo.CreateAsync(_mapper.Map<Country>(country));
            if (res != null)
            {
                await _countryRepo.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<CountryDTO>> GetAll()
        {
            List<Country> countries = (await _countryRepo.GetAllAsync()).ToList();
            return _mapper.Map<List<CountryDTO>>(countries);
        }

        public async Task<CountryDTO> GetById(int id)
        {
            return  _mapper.Map<CountryDTO>(await _countryRepo.GetByIdAsync(id));
        }

        public async Task<bool> UpdateCountry(CountryDTO country, int id)
        {
            Country country1 = _mapper.Map<Country>(country);
            country1.Id = id;
            var res = await _countryRepo.UpdateAsync(country1);
            if (res)
            {
                await _countryRepo.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
