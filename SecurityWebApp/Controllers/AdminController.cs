using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecurityWebApp.Attributes;
using SecurityWebApp.Data;
using SecurityWebApp.Data.Model;

namespace SecurityWebApp.Controllers
{
  [Roles(DefaultRoles.Max)]
  public class AdminController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger<AdminController> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signManager;

    public AdminController(ApplicationDbContext context, ILogger<AdminController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signManager)
    {
      _context = context;
      _logger = logger;
      _userManager = userManager;
      _signManager = signManager;
    }
    public async Task<IActionResult> Index() => View(await _context.Users.ToListAsync());

    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.Users
          .FirstOrDefaultAsync(m => m.Id == id.Value.ToString());

      if (user == null)
      {
        return NotFound();
      }

      return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var user = await _context.Users
          .FirstOrDefaultAsync(m => m.Id == id.Value.ToString());

      try
      {
        if (user.UserName.Equals(HttpContext.User.Identity.Name.ToString()))
          await _signManager.SignOutAsync();

        await _userManager.DeleteAsync(user);
      }
      catch (Exception)
      {
        return View("Error");
      }

      return RedirectToAction(nameof(Index));
    }
  }
}
