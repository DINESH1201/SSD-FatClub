using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;

namespace FatClub.Pages.Logs
{
    public class DeleteModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public DeleteModel(FatClub.Models.FatClubContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AuditLog = await _context.AuditLogs.FindAsync(id);

            if (AuditLog != null)
            {
                _context.AuditLogs.Remove(AuditLog);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
