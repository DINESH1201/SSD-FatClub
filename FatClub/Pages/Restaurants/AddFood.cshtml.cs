using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FatClub.Models;
using Microsoft.EntityFrameworkCore;

namespace FatClub.Pages.Restaurants
{
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
            auditrecord.Description = String.Format("Restaurant, {0}, with ID, {1}, has {2} with {3} to its menu by {4}", Restaurant.Name, Restaurant.RestaurantID, Food.Name, Food.Price, User.Identity.Name.ToString());
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);

            await _context.SaveChangesAsync();
            Response.Redirect("./Details?id=" + id);
            return null;
        }
    }
}