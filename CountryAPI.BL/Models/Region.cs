using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CountryAPI.BL.Models
{
    public class Region
    {
        [Key]
        [ForeignKey("Country")]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Country Country { get; set; }
    }
}
