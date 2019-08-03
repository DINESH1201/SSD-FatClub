using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FatClub.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Restaurants
{
    [Authorize(Roles = "Admin,Staff")]
    public class AddFoodModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public AddFoodModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }
        /*
        public IActionResult OnGet()
        {
            return Page();
        }
        */
        
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
            
            Food.RestaurantID = Convert.ToInt32(id);
            _context.Food.Add(Food);
            Restaurant = await _context.Restaurant.FirstOrDefaultAsync(m => m.RestaurantID == id);

            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "Food added to restaurant";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("{0} (${1}) added to {2}", Food.Name, Food.Price, Restaurant.Name);
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);

            await _context.SaveChangesAsync();
            Response.Redirect("./Details?id=" + id);
            return null;
        }
    }
}