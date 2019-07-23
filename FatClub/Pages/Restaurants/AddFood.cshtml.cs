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

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Food.RestaurantID = 1;
            //Food.FoodID = 2;
            _context.Food.Add(Food);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}