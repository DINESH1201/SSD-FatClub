using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace FatClub.Pages.Restaurants
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public IndexModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurant { get;set; }
        public IList<Rating> Rating { get; set; }
       // [ViewData]
        public List<double> RatingList = new List<double>();

        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }


        public async Task OnGetAsync()
        {
            
            var restaurants = from r in _context.Restaurant select r;
            if (!string.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(r => r.Genre.Contains(searchString) || r.Name.Contains(searchString));
            }
            Restaurant = await restaurants.ToListAsync();
            /*
            var ratinglist = from r in _context.Rating group r by r.RestaurantID into RestaurantGroup select new { AverageStar = RestaurantGroup.Average(x=> x.Star) };
            RatingList = new List<double>(await ratinglist.Distinct().ToListAsync());
            */
            foreach (Restaurant r in restaurants)
            {
                Rating = await _context.Rating.Where(rl => rl.RestaurantID == r.RestaurantID).ToListAsync();
                int calculated_rating = 0;
                if (Rating.Count() == 0)
                {
                    // Do Absolutely Nothing
                }
                else
                {
                    foreach (Rating ratings in Rating)
                    {
                        calculated_rating = ratings.Star + calculated_rating;
                    }
                    calculated_rating = Convert.ToInt32(calculated_rating / Rating.Count());
                }
                RatingList.Add(calculated_rating);
            }
            
        } 
    }
}