﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Foods
{
    [Authorize(Roles = "Admin, Staff")]
    public class CreateModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public CreateModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Food.Add(Food);

            if (await _context.SaveChangesAsync() > 0)
            {
                var auditrecord = new AuditLog();
                auditrecord.AuditActionType = "Add Food";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.FoodIDField = Food.ID;
                auditrecord.Description = String.Format("");
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;

                _context.AuditLogs.Add(auditrecord);
                await _context.SaveChangesAsync();
            }


           // await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}