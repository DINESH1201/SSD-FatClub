﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Restaurants
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public DetailsModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        public Restaurant Restaurant { get; set; }
        public IList<Food> Food { get; set; }

        public async Task OnPutAddToCartAsync(int FoodID)
        {
            var cartItem = new CartItem();
            cartItem.FoodID = FoodID;
            cartItem.Quantity = 1;
            String currentUserName = User.Identity.Name.ToString();
            ApplicationUser CurrentUser = await _context.Users.FirstOrDefaultAsync(m => m.UserName == currentUserName);
            cartItem.ShoppingCartID = CurrentUser.ShoppingCart.ShoppingCartID;
            _context.CartItems.Add(cartItem);
            
            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant = await _context.Restaurant.FirstOrDefaultAsync(m => m.RestaurantID == id);

            if (Restaurant == null)
            {
                return NotFound();
            }
            var foodlist = from r in _context.Food select r;
            foodlist = foodlist.Where(r => r.RestaurantID == id);
            // Food = await _context.Food.
            Food = await foodlist.ToListAsync();


            return Page();
        }
    }
}
