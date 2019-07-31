using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatClub.Models
{
    public class Rating
    {   [Key]
        public int RatingID { get ; set ; }
        //[Range(0, 5, ErrorMessage = "Please enter valid rating between 0-5")]
        public int Star { get; set; }
        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantID { get; set; }
    }
}
