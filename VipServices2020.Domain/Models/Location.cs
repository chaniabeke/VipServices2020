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
        public string Town { get; set; }

        public Location(string town)
        {
            Town = town;
        }
    }
}
