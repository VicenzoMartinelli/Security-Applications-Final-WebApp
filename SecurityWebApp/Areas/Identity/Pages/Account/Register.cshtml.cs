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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SecurityWebApp.Attributes;
using SecurityWebApp.Data.Model;

namespace SecurityWebApp.Areas.Identity.Pages.Account
{
  [AllowAnonymous]
  [IgnoreAntiforgeryToken]
  [Roles(DefaultRoles.Max)]
  public class RegisterModel : PageModel
  {
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ILogger<RegisterModel> _logger;
    private readonly IEmailSender _emailSender;

    public RegisterModel(
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ILogger<RegisterModel> logger,
        RoleManager<IdentityRole> roleManager,
        IEmailSender emailSender)
    {
      _userManager   = userManager;
      _signInManager = signInManager;
      _logger        = logger;
      _emailSender   = emailSender;
      _roleManager   = roleManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public SelectList Roles { get; set; }

    public string ReturnUrl { get; set; }

    public class InputModel
    {
      [Required(ErrorMessage = "Insira o email!")]
      [EmailAddress(ErrorMessage = "Insira um email válido!")]
      [Display(Name = "Email")]
      public string Email { get; set; }
      [Required(ErrorMessage = "Insira o username!")]
      [Display(Name = "Usuário")]
      public string UserName { get; set; }
      [Required(ErrorMessage = "Insira o setor!")]
      [Display(Name = "Setor")]
      public string Sector { get; set; }

      [Display(Name = "Tipo do usuário")]
      [Required]
      public string Role { get; set; }

      [Required(ErrorMessage = "Insira uma senha!")]
      [StringLength(100, ErrorMessage = "A {0} precisa ter no mínimo {2} e no máximo {1} caracteres", MinimumLength = 6)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      [DataType(DataType.Password)]
      [Display(Name = "Confirme a senha")]
      [Compare("Password", ErrorMessage = "As senhas não conferem!")]
      public string ConfirmPassword { get; set; }
    }

    public void OnGet(string returnUrl = null)
    {
      ReturnUrl = returnUrl;

      Roles = new SelectList(_roleManager.Roles, "Name", "Name");

    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
      returnUrl = returnUrl ?? Url.Content("~/Admin");
      if (ModelState.IsValid)
      {
        var user = new AppUser {
          UserName = Input.UserName,
          Email = Input.Email,
          Sector = Input.Sector
        };
        var result = await _userManager.CreateAsync(user, Input.Password);
        if (result.Succeeded)
        {
          _logger.LogInformation($"Usuário {user.UserName} cadastrado");

          await _userManager.AddToRoleAsync(user, Input.Role);
          
          return Redirect(returnUrl);
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
