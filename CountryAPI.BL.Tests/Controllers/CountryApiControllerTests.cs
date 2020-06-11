using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountryAPI.BL.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CountryAPI.BL.Database;
using CountryAPI.BL.Models;
using CountryAPI.BL.Database.Repository;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using CountryAPI.BL.ViewModels;
using Newtonsoft.Json.Converters;

namespace CountryAPI.BL.Controllers.Tests
{
    [TestClass()]
    public class CountryApiControllerTests
    {
        private readonly static CountryApiContext _context = new CountryApiContext();
        private readonly static CountryApiController _controller = new CountryApiController(new CountryApiRepository(_context));
        HttpWebRequest request;
        HttpWebResponse response;

        [TestMethod()]
        public void GetCountryTest()
        {
            string countryUrl = "https://restcountries.eu/rest/v2/name/russia";
            CountryRegionCity country = null;
            CountryRegionCity testCountry = _controller.GetCountry("Russia");
            string countryData = "";
            string testCountryData = "";
            request = (HttpWebRequest)WebRequest.Create(countryUrl);
            response = (HttpWebResponse)request.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
                countryData = sr.ReadToEnd();
            country = JsonConvert.DeserializeObject<List<CountryRegionCity>>(countryData).First();
            countryData = JsonConvert.SerializeObject(country);
            testCountryData = JsonConvert.SerializeObject(testCountry);

            Assert.AreEqual(countryData, testCountryData);
        }

        [TestMethod()]
        public void GetAllCountriesTest()
        {
            IEnumerable<Country> countries = null;
            IEnumerable<Country> testCountries = null;
            bool isEquals = false;

            countries = _context.Countries.Include("Region").Include("City").ToList();
            testCountries = _controller.GetAllCountries().ToList();
            isEquals = countries.SequenceEqual(testCountries);

            Assert.IsTrue(isEquals);
        }

        [TestMethod()]
        public void CreateCountryTest()
        {
            string countryCode = "RU";
            string countryName = "Russia";
            bool isAdded = false;

            _controller.CreateCountry(countryName);
            Country country = _context.Countries.FirstOrDefault(c => c.Code == countryCode);
            if (country != null)
            {
                isAdded = true;
            }

            Assert.IsTrue(isAdded);
        }
    }
}