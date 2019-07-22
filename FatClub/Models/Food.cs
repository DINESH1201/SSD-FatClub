using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FatClub.Models
{
    public class Food
    {   [Key]
        public int FoodID { get; set; }
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please enter a proper name. Ensure no special characters or numbers.")]
        public string Name { get; set; }
        [RegularExpression(@"^[0-9]{4,0}+\.[0-9]{2}$", ErrorMessage = "Price cannot have more than 2 decimal places")]
        public decimal Price { get; set; }
        public Restaurant Restaurant { get; set; }
        public int RestaurantID { get; set; }

    }
}
