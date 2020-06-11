using CountryAPI.BL.Models;
using CountryAPI.BL.ViewModels;
using System;
using System.Collections.Generic;

namespace CountryAPI.BL.Interfaces
{
    public interface ICountryApi
    {
        CountryRegionCity CountryByName(string name);
        IEnumerable<Country> Countries { get; }
        void AddCountry(string name);
    }
}
