using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Town { get; set; }

        public Location(string town)
        {
            Town = town;
        }
        public override string ToString()
        {
            return $"{Town}";
        }
    }
}
