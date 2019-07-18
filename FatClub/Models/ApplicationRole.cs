using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FatClub.Models
{
    public class ApplicationRole : IdentityRole
    {
        [DataType(DataType.MultilineText)]
        [StringLength(150)]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }

    }
}
