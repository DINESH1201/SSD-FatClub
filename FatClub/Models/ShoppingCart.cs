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
        [ForeignKey("ApplicationUser")]
        public string UserName { get; set; }
    }
}
