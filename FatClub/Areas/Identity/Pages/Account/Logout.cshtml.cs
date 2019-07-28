using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FatClub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace FatClub.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LogoutModel> _logger;
        private readonly FatClub.Models.FatClubContext _context;
        private IList<CartItem> CartItems { get; set; }

        public LogoutModel(SignInManager<ApplicationUser> signInManager, ILogger<LogoutModel> logger, FatClub.Models.FatClubContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            String currentUsername = User.Identity.Name;
            ShoppingCart cart = await _context.ShoppingCarts.FirstOrDefaultAsync(m => m.UserName == currentUsername);
            
            CartItems = await _context.CartItems.Where(item => item.ShoppingCartID == cart.ShoppingCartID).ToListAsync();

            foreach (CartItem items in CartItems)
            {
                _context.CartItems.Remove(items);
            }

            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "Cart cleared";
            auditrecord.DateTimeStamp = DateTime.Now;
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            auditrecord.Description = String.Format("{0}'s cart has been cleared.", userID);
            _context.AuditLogs.Add(auditrecord);

            await _context.SaveChangesAsync();

            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return Page();
            }
        }
    }
}