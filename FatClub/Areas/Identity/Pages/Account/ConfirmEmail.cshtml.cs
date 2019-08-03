using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FatClub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FatClub.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FatClub.Models.FatClubContext _context;

        public ConfirmEmailModel(UserManager<ApplicationUser> userManager, FatClub.Models.FatClubContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");
            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "User Email Confirmed";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("User with ID {0} has confirmed his email.", user.UserName);
            auditrecord.Username = user.UserName;
            //auditrecord.Username = User.Identity.Name.ToString();
            _context.AuditLogs.Add(auditrecord);
            await _context.SaveChangesAsync();

            if (roleResult.Succeeded)
            {
                 // return Page();
            }
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Error confirming email for user with ID '{userId}':");
            }

            return Page();
        }
    }
}
