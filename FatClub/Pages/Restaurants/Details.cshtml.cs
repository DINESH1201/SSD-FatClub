using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

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

       // public async Task<IActionResult> OnGetAddToCart(int FoodID)
        //{

          
           // return null;
        //}

        public async Task<IActionResult> OnGetAddToCartAsync(int FoodID, int? id)
        {
            String currentUserName = User.Identity.Name;
            
            var cartItem = new CartItem();
            cartItem.FoodID = FoodID;
            cartItem.Quantity = 1;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUserName);
            cartItem.ShoppingCartID = cart.ShoppingCartID;
            _context.CartItems.Add(cartItem);

            await _context.SaveChangesAsync();
            
            return await OnGetAsync(id);
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
