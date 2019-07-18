using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FatClub.Models
{
    public class Rating
    {
        public int ID { get ; set ; }
        [StringLength(150)]
        public string Description { get ; set ; }
    }
}
