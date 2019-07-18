using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FatClub.Models
{
    public class ApplicationUser : IdentityUser
    {   [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(8)]
        [RegularExpression(@"^[0-9]*$")]
        public string MobileNo { get; set; }
    }

}
