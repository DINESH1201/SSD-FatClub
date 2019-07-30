using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Cart
{
    [Authorize]
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

        public async Task<IActionResult> OnGetCheckoutAsync()
        {
            String currentUsername = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUsername);
            CartItem = await _context.CartItems.Where(item => item.ShoppingCartID == cart.ShoppingCartID).ToListAsync();

            foreach(CartItem items in CartItem)
            {
                _context.CartItems.Remove(items);
            }

            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "User has checked out";
            auditrecord.DateTimeStamp = DateTime.Now;
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            auditrecord.Description = String.Format("{0} has made an order.", userID);
            _context.AuditLogs.Add(auditrecord);

            await _context.SaveChangesAsync();

            
            return RedirectToPage("./Checkout");
        }


        public IList<CartItem> CartItem { get; set; }
        public IList<Food> Food = new List<Food>();
        public string Total { get; set; }



        public async Task OnGetAsync()
        {
            String currentUsername = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUsername);
            CartItem = await _context.CartItems.Where(item => item.ShoppingCartID == cart.ShoppingCartID).ToListAsync();
            decimal total = 0;
            foreach (CartItem items in CartItem)
            {
                Food food = await _context.Food.FirstOrDefaultAsync(foo => foo.FoodID == items.FoodID);
                total += items.Quantity * food.Price;
                Food.Add(food);
            }

            Total = String.Format("${0}", total.ToString("0.##"));

        }
    }
}
