using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;

namespace FatClub.Pages.Cart
{
    public class ViewCartModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public ViewCartModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetDeleteFromCartAsync(int id)
        {

            CartItem cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            await OnGetAsync();

            return Page();
        }

        public IList<CartItem> CartItem { get;set; }
        public decimal Total { get; set; }
        
        public async Task OnGetAsync()
        {
            String currentUsername = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUsername);
            CartItem = await _context.CartItems.Where(item => item.ShoppingCartID == cart.ShoppingCartID).ToListAsync();
            Total = 0;
            foreach(CartItem items in CartItem)
            {
                Food food = await _context.Food.FirstOrDefaultAsync(foo => foo.FoodID == items.FoodID);
                Total += items.Quantity * food.Price;
            }
            Total += 0;
            
        }
    }
}
