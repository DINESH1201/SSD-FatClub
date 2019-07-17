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

namespace FatClub.Pages.Logs
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
        public AuditLog AuditLog { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AuditLog = await _context.AuditLogs.FirstOrDefaultAsync(m => m.Audit_ID == id);

            if (AuditLog == null)
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

            _context.Attach(AuditLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuditLogExists(AuditLog.Audit_ID))
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

        private bool AuditLogExists(int id)
        {
            return _context.AuditLogs.Any(e => e.Audit_ID == id);
        }
    }
}