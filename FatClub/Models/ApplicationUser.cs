using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace FatClub.Models
{
    public class ApplicationUser : IdentityUser
    {   [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        
        //public int CartID { get; set; }
        //public virtual ShoppingCart ShoppingCart { get; set; }

    }

}
