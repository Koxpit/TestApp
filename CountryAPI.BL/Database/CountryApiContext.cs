using CountryAPI.BL.Models;
using System;
using System.Data.Entity;

namespace CountryAPI.BL.Database
{
    public class CountryApiContext : DbContext
    {
        public CountryApiContext() : base("CountryApiDB")
        { }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
    }
}
