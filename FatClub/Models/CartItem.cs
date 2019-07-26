using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FatClub.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemID { get; set; }
       
        public int Quantity { get; set; }

        [ForeignKey("Food")]
        public int FoodID { get; set; }


        [ForeignKey("ShoppingCart")]
        public int ShoppingCartID { get; set; }
    }
}
