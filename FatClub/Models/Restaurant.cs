using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace FatClub.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Genre { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public List<Food> FoodList { get ; set ; }
        public List<Rating> RatingList { get ; set ; }
    }
}
