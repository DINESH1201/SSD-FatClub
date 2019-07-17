using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Logs
{
    [Authorize(Roles = "Admin")]
    public class DetailsModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public DetailsModel(FatClub.Models.FatClubContext context)
        {
            _context = context;
        }

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
    }
}
