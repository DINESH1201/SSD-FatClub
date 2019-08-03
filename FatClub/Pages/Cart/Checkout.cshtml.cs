using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FatClub.Models;
using Microsoft.EntityFrameworkCore;

namespace FatClub.Pages.Cart
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;
        public CheckoutModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }
       
        public async Task OnPostAsync()
        {  
            var StarRating = Request.Form["starrating"];
            String currentUsername = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUsername);
            Restaurant restaurant = await _context.Restaurant.FirstOrDefaultAsync(item => item.RestaurantID == cart.RestaurantID);

            var newrating = new Rating() { RestaurantID = restaurant.RestaurantID , Star = Convert.ToInt32(StarRating) };
            _context.Rating.Add(newrating);

            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "Rating added";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("{0} has added a rating to {1}", currentUsername, restaurant.Name);
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);

            await _context.SaveChangesAsync();
        }

        
    }
}