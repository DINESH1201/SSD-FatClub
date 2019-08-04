using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FatClub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.IdentityModel.Protocols;
using System.IO;
using Newtonsoft.Json.Linq;

namespace FatClub.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly FatClub.Models.FatClubContext _context;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            FatClub.Models.FatClubContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
            [DataType(DataType.Password)]
    
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "First Name")]
            [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter valid string.")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter valid string.")]
            public string LastName { get; set; }

            [Required]
            [RegularExpression(@"^([8-9]{1})(\d{7})$", ErrorMessage = "Enter a proper mobile no. Starts with 8 or 9 and 7 other digits.")]
            [Display(Name = "Mobile No")]   
            public string PhoneNumber { get; set; }

        }


        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }
        
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Identity/Account/Login");
            if (ModelState.IsValid)
            {
                var shoppingCart = new ShoppingCart();
                shoppingCart.UserName = Input.Email;
                _context.ShoppingCarts.Add(shoppingCart);
                await _context.SaveChangesAsync();

                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, PhoneNumber = Input.PhoneNumber };//, ShoppingCart = shoppingCart };
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");


                    var auditrecord = new AuditLog();
                    auditrecord.AuditActionType = "New User Account Created";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.Description = String.Format("Account with email {0} has been created by {1} {2}.", Input.Email, Input.FirstName, Input.LastName);
                    auditrecord.Username = Input.Email;
                    _context.AuditLogs.Add(auditrecord);
                    await _context.SaveChangesAsync();

                    //await _signInManager.SignInAsync(user, isPersistent: false);                                                     Prevent newly registered users from being automatically signed       

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }



            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
