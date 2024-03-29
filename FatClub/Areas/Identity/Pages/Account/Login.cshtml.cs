﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using FatClub.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FatClub.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly FatClub.Models.FatClubContext _context;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, FatClub.Models.FatClubContext context)
        {
            _signInManager = signInManager;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    var auditrecord = new AuditLog();
                    auditrecord.AuditActionType = "User Login";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.Description = String.Format("Account with email {0} has logged in.", Input.Email);
                    auditrecord.Username = Input.Email;
                    //auditrecord.Username = User.Identity.Name.ToString();
                    _context.AuditLogs.Add(auditrecord);
                    await _context.SaveChangesAsync();
                    return LocalRedirect(returnUrl);
                }
                else
                {
                    var auditrecord = new AuditLog();
                    auditrecord.AuditActionType = "Failed Login";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.Description = String.Format("Account with email {0} failed to log in.", Input.Email);
                    auditrecord.Username = Input.Email;
                    _context.AuditLogs.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    var auditrecord = new AuditLog();
                    auditrecord.AuditActionType = "Account locked out";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.Description = String.Format("Account with email {0} has been locked out.", Input.Email);
                    auditrecord.Username = Input.Email;
                    _context.AuditLogs.Add(auditrecord);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
