using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FatClub.Models
{
    public class Restaurant
    {   [Key]
        public int RestaurantID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Genre { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        public ICollection<Food> Foods { get; set; }
        public ICollection<Rating> Ratings { get; set; }



    }
}
