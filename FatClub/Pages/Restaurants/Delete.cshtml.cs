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
    public class DeleteModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public DeleteModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; }

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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Restaurant = await _context.Restaurant.FindAsync(id);

            if (Restaurant != null)
            {
                var auditrecord = new AuditLog();
                auditrecord.AuditActionType = "Restaurant Deleted";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.Description = String.Format("{0} was deleted by {1}.", Restaurant.Name, User.Identity.Name.ToString());
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;
                _context.AuditLogs.Add(auditrecord);

                _context.Restaurant.Remove(Restaurant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
