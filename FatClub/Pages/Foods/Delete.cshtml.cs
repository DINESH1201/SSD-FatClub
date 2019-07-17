using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FatClub.Models;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Foods
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly FatClub.Models.FatClubContext _context;

        public DeleteModel(FatClub.Models.FatClubContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Food = await _context.Food.FindAsync(id);

            if (Food != null)
            {
                _context.Food.Remove(Food);
                if (await _context.SaveChangesAsync() > 0)
                {
                    var auditrecord = new AuditLog();
                    auditrecord.AuditActionType = "Delete Food";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.FoodIDField = Food.ID;
                    auditrecord.Description = String.Format("");
                    var userID = User.Identity.Name.ToString();
                    auditrecord.Username = userID;

                    _context.AuditLogs.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
