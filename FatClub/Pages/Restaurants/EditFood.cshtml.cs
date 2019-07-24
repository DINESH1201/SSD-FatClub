﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Restaurants
{
    [Authorize(Roles = "Admin, Staff")]
    public class EditFoodModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public EditFoodModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

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

            if (Food == null)
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

            _context.Attach(Food).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(Food.FoodID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "Food deleted";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("{0} in {2} was edited by by {1}.", Food.Name, User.Identity.Name.ToString(), Restaurant.Name);
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);

            Response.Redirect("./Details?id=" + id);
            return null;
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.FoodID == id);
        }
    }
}
