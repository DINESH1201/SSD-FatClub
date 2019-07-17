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

namespace FatClub.Pages.Foods
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public EditModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Food Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Food = await _context.Food.FirstOrDefaultAsync(m => m.ID == id);

            if (Food == null)
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

            _context.Attach(Food).State = EntityState.Modified;

            try
            {
                if (await _context.SaveChangesAsync() > 0)
                {
                    var auditrecord = new AuditLog();
                    auditrecord.AuditActionType = "Edit Food";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.FoodIDField = Food.ID;
                    auditrecord.Description = String.Format("");
                    var userID = User.Identity.Name.ToString();
                    auditrecord.Username = userID;

                    _context.AuditLogs.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }
//                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodExists(Food.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool FoodExists(int id)
        {
            return _context.Food.Any(e => e.ID == id);
        }
    }
}
