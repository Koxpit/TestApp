using CountryAPI.BL.Interfaces;
using CountryAPI.BL.Models;
using CountryAPI.BL.ViewModels;
using System;
using System.Collections.Generic;

namespace CountryAPI.BL.Controllers
{
    public class CountryApiController
    {
        private readonly ICountryApi _countryApi;

        public CountryApiController(ICountryApi countryApi)
        {
            _countryApi = countryApi;
        }

        public CountryRegionCity GetCountry(string name)
        {
            return _countryApi.CountryByName(name);
        }

        public IEnumerable<Country> GetAllCountries()
        {
            return _countryApi.Countries;
        }

        public void CreateCountry(string countryName)
        {
            _countryApi.AddCountry(countryName);
        }
    }
}
