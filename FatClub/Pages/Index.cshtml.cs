﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FatClub.Pages
{
    public class IndexModel : PageModel
    {
        private readonly FatClub.Models.Restaurant _context;
        public IndexModel(FatClub.Models.Restaurant context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public void OnGet()
        {

        }
    }
}
