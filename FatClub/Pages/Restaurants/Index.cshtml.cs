using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;
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
        [ViewData]
        public IList<int> RatingList { get; set; }

        [BindProperty(SupportsGet = true)]
        public string searchString { get; set; }


        public async Task OnGetAsync(IList<int> RatingList)
        {
            var restaurants = from r in _context.Restaurant select r;
            if (!string.IsNullOrEmpty(searchString))
            {
                restaurants = restaurants.Where(r => r.Genre.Contains(searchString) || r.Name.Contains(searchString));
            }
            restaurants = await _context.Restaurant.ToListAsync();
            var ratinglist = from r in _context.Rating select r;
            ratinglist = await _context.Rating.ToListAsync();

            foreach (var r in restaurants)
            {
                ratinglist = ratinglist.Where(rl => rl.RestaurantID == r.RestaurantID);
                int calculated_rating = 0;
                if (ratinglist.Count() == 0)
                {
                    // Do Absolutely Nothing
                }
                else
                {
                    foreach (var rl in ratinglist)
                    {
                        calculated_rating = rl.Star + calculated_rating;
                    }
                    calculated_rating = Convert.ToInt32(calculated_rating / ratinglist.Count());
                }
                RatingList.Add(calculated_rating);
            }
        } 
    }
}