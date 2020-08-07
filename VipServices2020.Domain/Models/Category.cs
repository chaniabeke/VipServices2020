using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VipServices2020.Domain.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string CategoryName { get; set; }

        public Category()
        {

        }
        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
        public override string ToString()
        {
            return $"{CategoryName}";
        }
    }
}
