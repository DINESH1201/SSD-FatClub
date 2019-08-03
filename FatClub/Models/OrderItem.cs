using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatClub.Models
{
    public class OrderItem
    {
        [Key]
        public int OrderItemID { get; set; }

        [Range(1, 5)]
        public int Quantity { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderID { get; set; }

        [Required]
        [ForeignKey("Food")]
        public int FoodID { get; set; }

    }
}
