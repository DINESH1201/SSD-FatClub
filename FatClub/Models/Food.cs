using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FatClub.Models
{
    public class Food
    {
        public int ID { get; set; }
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "Please enter a proper name. Ensure no special characters or numbers.")]
        public string Name { get; set; }
        [RegularExpression(@"^[0-9]{4,0}+\.[0-9]{2}$")]
        public decimal Price { get; set; }

    }
}
