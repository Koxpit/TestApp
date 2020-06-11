using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryAPI.BL.ViewModels
{
    public class CountryRegionCity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("alpha2Code")]
        public string Code { get; set; }

        [JsonProperty("area")]
        public double? Area { get; set; }

        [JsonProperty("population")]
        public int PeopleCount { get; set; }

        [JsonProperty("capital")]
        public string Capital { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }
    }
}
