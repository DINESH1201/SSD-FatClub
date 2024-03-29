﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FatClub.Models
{
    public class Food
    {
        [Key]
        public int FoodID { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter a proper name. Ensure no special characters or numbers.")]
        public string Name { get; set; }

        [RegularExpression(@"^[0-9]+(\.[0-9]{1,2})?$", ErrorMessage = "Invalid price entered")]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Restaurant")]
        public int RestaurantID { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        public static implicit operator Food(List<Food> v)
        {
            throw new NotImplementedException();
        }
    }
}
