using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FatClub.Models;

namespace FatClub.Pages.Restaurants
{
    public class CreateModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public CreateModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Restaurant.Add(Restaurant);

            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "New restaurant created";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("New restaurant named {0} was created by {2}. Description is '{1}'", Restaurant.Name, Restaurant.RestaurantID, User.Identity.Name.ToString());
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}