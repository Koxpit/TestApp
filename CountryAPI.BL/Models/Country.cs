using System;

namespace CountryAPI.BL.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public double? Area { get; set; }
        public int PeopleCount { get; set; }
        public int RegionId { get; set; }
        public int CityId { get; set; }

        public virtual Region Region { get; set; }
        public virtual City City { get; set; }
    }
}
