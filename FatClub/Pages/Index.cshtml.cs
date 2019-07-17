using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FatClub.Models;

namespace FatClub.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public IndexModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }
    }
}
