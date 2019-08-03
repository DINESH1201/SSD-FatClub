using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatClub.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public bool RatingDone { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserName { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int? RestaurantID { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
