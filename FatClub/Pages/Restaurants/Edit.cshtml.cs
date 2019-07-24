using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Restaurants
{
    [Authorize(Roles = "Admin, Staff")]
    public class EditModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public EditModel(FatClub.Models.FatClubContext context)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(Restaurant.RestaurantID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "Restaurant Edited";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("{0} was edited by {1}.", Restaurant.Name, User.Identity.Name.ToString());
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);

            Response.Redirect("./Details?id=" + id);
            return null;
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.RestaurantID == id);
        }
    }
}
