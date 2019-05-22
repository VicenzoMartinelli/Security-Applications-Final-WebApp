using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SecurityWebApp.Data.Model;

namespace SecurityWebApp.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  [IgnoreAntiforgeryToken]
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;

    public RegisterModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ILogger<RegisterModel> logger,
        IEmailSender emailSender)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _logger = logger;
      _emailSender = emailSender;
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
      [StringLength(100, ErrorMessage = "A {0} precisa ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = "As senhas não conferem!")]
      public string ConfirmPassword { get; set; }
    }

    public void OnGet(string returnUrl = null)
    {
      ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl = returnUrl ?? Url.Content("~/");
      if (ModelState.IsValid)
      {
        var user = new AppUser { UserName = Input.Email, Email = Input.Email };
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

          await _emailSender.SendEmailAsync(Input.Email, "Confirme seu email!",
              $"Por favor confirme seu email <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicando aqui</a>.");

          await _signInManager.SignInAsync(user, isPersistent: false);
          return LocalRedirect(returnUrl);
        }
        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }
      return Page();
    }
  }
}
