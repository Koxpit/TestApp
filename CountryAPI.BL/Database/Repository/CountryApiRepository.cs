using CountryAPI.BL.Interfaces;
using CountryAPI.BL.Models;
using CountryAPI.BL.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;

namespace CountryAPI.BL.Database.Repository
{
    public class CountryApiRepository : ICountryApi
    {
        private readonly string baseUrl = "https://restcountries.eu/rest/v2";
        private readonly CountryApiContext _context;

        public CountryApiRepository(CountryApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Country> Countries => _context.Countries.Include("Region").Include("City").ToList();

        public CountryRegionCity CountryByName(string name)
        {
            string getCountyInfoUrl = $"{baseUrl}/name/{name}";
            string responseData = "";

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(getCountyInfoUrl);
                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    responseData = sr.ReadToEnd();
                }
            } catch
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<CountryRegionCity>>(responseData).First();
        }

        public void AddCountry(string name)
        {
            CountryRegionCity countryViewModel = CountryByName(name);
            Country country = _context.Countries.FirstOrDefault(c => c.Code == countryViewModel.Code);

            if (country == null)
            {
                _context.Countries.Add(new Country
                {
                    Name = countryViewModel.Name,
                    Code = countryViewModel.Code,
                    Area = countryViewModel.Area,
                    CityId = 0,
                    RegionId = 0,
                    PeopleCount = countryViewModel.PeopleCount,
                });
            }
            else
            {
                UpdateData(countryViewModel, country);
            }

             _context.SaveChanges();
        }

        private void UpdateData(CountryRegionCity countryViewModel, Country country)
        {
            if (country.CityId == 0)
            {
                _context.Cities.Add(new City { Id = country.Id, Name = countryViewModel.Capital });
            }

            if (country.RegionId == 0)
            {
                _context.Regions.Add(new Region { Id = country.Id, Name = countryViewModel.Region });
            }

            _context.SaveChanges();

            country.CityId = _context.Cities.FirstOrDefault(n => n.Name == countryViewModel.Capital).Id;
            country.RegionId = _context.Regions.FirstOrDefault(n => n.Name == countryViewModel.Region).Id;

            _context.Entry(country).State = EntityState.Modified;
        }
    }
}
