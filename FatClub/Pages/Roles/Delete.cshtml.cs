using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FatClub.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly FatClub.Models.FatClubContext _context;

        public DeleteModel(RoleManager<ApplicationRole> roleManager,
            FatClub.Models.FatClubContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        [BindProperty]
        public ApplicationRole ApplicationRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole = await _roleManager.FindByIdAsync(id);

            if (ApplicationRole == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApplicationRole = await _roleManager.FindByIdAsync(id);
            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "Role Deleted";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("Role named {0} with ID {1} was deleted.", ApplicationRole.Name, ApplicationRole.Id);
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);
            await _context.SaveChangesAsync();
            IdentityResult roleRuslt = await _roleManager.DeleteAsync(ApplicationRole);
            return RedirectToPage("./Index");

        }
    }
}
