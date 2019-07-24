using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;

namespace FatClub.Pages.Restaurants
{
    public class DeleteFoodModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public DeleteFoodModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Food Food { get; set; }
        private Restaurant Restaurant { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant = await _context.Restaurant.FirstOrDefaultAsync(m => m.RestaurantID == id);

            if (Food == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Food = await _context.Food.FindAsync(id);

            if (Food != null)
            {
                Restaurant = await _context.Restaurant.FirstOrDefaultAsync(m => m.RestaurantID == id);

                var auditrecord = new AuditLog();
                auditrecord.AuditActionType = "Food deleted";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.Description = String.Format("{0} was deleted from {2} by {1}.", Food.Name, User.Identity.Name.ToString(),Restaurant.Name);
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;
                _context.AuditLogs.Add(auditrecord);

                _context.Food.Remove(Food);
                await _context.SaveChangesAsync();
            }

            Response.Redirect("./Details?id=" + id);
            return null;
        }
    }
}
