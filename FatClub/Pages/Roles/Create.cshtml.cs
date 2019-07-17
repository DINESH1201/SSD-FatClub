using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FatClub.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace FatClub.Pages.Roles
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly FatClub.Models.FatClubContext _context;

        public CreateModel(RoleManager<ApplicationRole> roleManager,
            FatClub.Models.FatClubContext context)
        {
            _roleManager = roleManager;
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApplicationRole ApplicationRole { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ApplicationRole.CreatedDate = DateTime.UtcNow;
            ApplicationRole.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            var auditrecord = new AuditLog();
            auditrecord.AuditActionType = "New Role Created";
            auditrecord.DateTimeStamp = DateTime.Now;
            auditrecord.FoodIDField = 0;
            auditrecord.Description = String.Format("");
            var userID = User.Identity.Name.ToString();
            auditrecord.Username = userID;
            _context.AuditLogs.Add(auditrecord);
            await _context.SaveChangesAsync();

            IdentityResult roleRuslt = await _roleManager.CreateAsync(ApplicationRole);

            return RedirectToPage("Index");
        }
    }
}
