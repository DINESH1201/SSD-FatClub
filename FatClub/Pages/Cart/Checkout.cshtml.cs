using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FatClub.Models;


namespace FatClub.Pages.Cart
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;
        public ViewCartModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }
       
        public async OnPostAsync()
        {  
            var StarRating = Request.Form["starrating"];
            String currentUsername = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUsername);
            CartItem cartitem = await _context.CartItems.FirstOrDefaultAsync(item => item.ShoppingCartID == cart.ShoppingCartID);
            Food food = await _context.Food.FirstOrDefaultAsync(item => item.FoodID == cartitem.FoodID);
            Restaurant restaurant = await _context.Restaurant.FirstOrDefaultAsync(item => item.RestaurantID == food.RestaurantID);

            var newrating = new Rating() { RestaurantID = restaurant.RestaurantID , Star = StarRating };
            _context.Rating.Add(newrating);
        }


    }
}