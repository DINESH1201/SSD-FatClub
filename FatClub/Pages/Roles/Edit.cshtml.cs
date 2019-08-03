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
    public class EditModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly FatClub.Models.FatClubContext _context;

        public EditModel(RoleManager<ApplicationRole> roleManager,
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationRole appRole = await _roleManager.FindByIdAsync(ApplicationRole.Id);

            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "Role Updated";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.Description = String.Format("{0} ({1}) changed to {2} ({3})", appRole.Name, appRole.Description ,ApplicationRole.Name, ApplicationRole.Description);
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);
            await _context.SaveChangesAsync();

            appRole.Id = ApplicationRole.Id;
            appRole.Name = ApplicationRole.Name;
            appRole.Description = ApplicationRole.Description;

            

            IdentityResult roleRuslt = await _roleManager.UpdateAsync(appRole);

            if (roleRuslt.Succeeded)
            {
                return RedirectToPage("./Index");

            }
            return RedirectToPage("./Index");
        }

    }
}
