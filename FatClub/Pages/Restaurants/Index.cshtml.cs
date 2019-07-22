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
    public class IndexModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public IndexModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurant { get;set; }
        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }


        public async Task OnGetAsync()
        {
            var restaurants = from r in _context.Restaurant select r;
            if (!string.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(r => r.Genre.Contains(searchString) || r.Name.Contains(searchString));
            }
            Restaurant = await restaurants.Include(r => r.Foods).Include(r => r.Ratings).ToListAsync();
       
        }
    }
}
