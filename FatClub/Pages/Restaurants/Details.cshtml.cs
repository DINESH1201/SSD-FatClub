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
        public bool MsgBoxString { get; set; }
       // public async Task<IActionResult> OnGetAddToCart(int FoodID)
        //{

          
           // return null;
        //}

        public async Task<IActionResult> OnPostAddToCartAsync(int FoodID, int? id)
        {
            String currentUserName = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUserName);

            IList<CartItem> cartItems = new List<CartItem>();
            cartItems = await _context.CartItems.Where(m => m.ShoppingCartID == cart.ShoppingCartID).ToListAsync();
            
            Food food = _context.Food.FirstOrDefault(m => m.FoodID == FoodID);

            if (cartItems.Count == 0)
            {
                cart.RestaurantID = food.RestaurantID;
                _context.ShoppingCarts.Update(cart);

            }


            if (cart.RestaurantID == food.RestaurantID)
            {
                string n = String.Format("{0}", Request.Form[String.Format("quantity-{0}", FoodID)]);
                var cartItem = new CartItem();
                cartItem.FoodID = FoodID;
                cartItem.Quantity = Convert.ToInt32(n);
                cartItem.ShoppingCartID = cart.ShoppingCartID;
                _context.CartItems.Add(cartItem);
                MsgBoxString = true;//"Your food as been added to the cart";
            }
            else
            {
                MsgBoxString = false; //"You can only order food from 1 restaurant. Sorry for the inconvenience";
                
                
            }


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
