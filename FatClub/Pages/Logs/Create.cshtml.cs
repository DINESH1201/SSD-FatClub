using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Logs
{
    [Authorize(Roles = "Admin")]
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
        public AuditLog AuditLog { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AuditLogs.Add(AuditLog);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}