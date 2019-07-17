using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FatClub.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public List<Food> FoodList { get ; set ; }
        public List<Rating> RatingList { get ; set ; }
    }
}
