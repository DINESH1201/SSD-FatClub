﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FatClub.Pages.Cart
{
    [Authorize]
    public class CheckoutModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}