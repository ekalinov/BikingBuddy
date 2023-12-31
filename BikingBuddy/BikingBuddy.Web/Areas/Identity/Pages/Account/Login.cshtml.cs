﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using BikingBuddy.Data.Models;
using BikingBuddy.Services.Contracts;
using static BikingBuddy.Common.EntityValidationsConstants.User;
using static BikingBuddy.Common.ErrorMessages.UserErrorMessages;

namespace BikingBuddy.Web.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IUserService _userService;

        public LoginModel(SignInManager<AppUser> signInManager, ILogger<LoginModel> logger, IUserService userService)
        {
            _signInManager = signInManager;
            _logger = logger;
            _userService = userService;
        }

        [BindProperty] public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        public string ReturnUrl { get; set; }


        [TempData] public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [RegularExpression(@"^[\w\d_!@]{0,}$",
                ErrorMessage = "Username can be only alphabetic character,numbers, _, ! and @!")]
            [MaxLength(UsernameMaxLength)]
            [Display(Name = "Username")]
            public string Username { get; set; }


            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }


            [Display(Name = "Remember me?")] public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (await _userService.IsDeletedByUsernameAsync(Input.Username))
            {
               ErrorMessage= DeletedUsersCantLogin;
                return Page();
            }
            
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(Input.Username, Input.Password,
                        Input.RememberMe,
                        lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }

                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa",
                            new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                    }

                    if (result.IsLockedOut)
                    {
                        _logger.LogWarning("User account locked out.");
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