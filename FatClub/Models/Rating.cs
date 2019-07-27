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
        public int Star { get; set; }
        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantID { get; set; }
    }
}
