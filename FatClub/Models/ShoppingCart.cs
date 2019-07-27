using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatClub.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartID { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserName { get; set; }

//        public virtual ApplicationUser ApplicationUser { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
