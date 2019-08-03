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

            return null;
        }

        public async Task<IActionResult> OnGetCheckoutAsync()
        {
            String currentUsername = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUsername);
            CartItem = await _context.CartItems.Where(item => item.ShoppingCartID == cart.ShoppingCartID).ToListAsync();

            if (CartItem.Count > 0) {   
                var neworder = new Order() { UserName = currentUsername, RestaurantID = cart.RestaurantID, RatingDone = false };
                _context.Order.Add(neworder);
                foreach(CartItem items in CartItem)
                {
                    var orderitem = new OrderItem() { OrderID = neworder.OrderID, Quantity = items.Quantity, FoodID = items.FoodID };
                    _context.OrderItems.Add(orderitem);
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
            else
            {
                ViewData["output"] = "no items";
                await OnGetAsync();
                return null;
            }
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
