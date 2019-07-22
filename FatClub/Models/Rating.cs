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
        [StringLength(150)]
        public string Description { get ; set ; }
        [RegularExpression("^[0-5]{1}$")]
        public int Star { get; set; }
        [ForeignKey("Restaurant")]
        public int RestaurantID { get; set; }
    }
}
