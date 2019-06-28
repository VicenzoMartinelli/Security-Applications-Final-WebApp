using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityWebApp.Attributes;
using SecurityWebApp.Data;
using SecurityWebApp.Data.Model;

namespace SecurityWebApp.Controllers
{
  [Authorize]
  public class PilotsController : Controller
  {
    private readonly ApplicationDbContext _context;

    public PilotsController(ApplicationDbContext context)
    {
      _context = context;
    }


    public async Task<IActionResult> Index()
    {
      return View(await _context.Pilots.ToListAsync());
    }

    [Roles(DefaultRoles.Max)]
    public IActionResult Create()
    {
      return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Roles(DefaultRoles.Max)]
    public async Task<IActionResult> Create([Bind("Id,Name,BirthDate,Genre")] Pilot pilot)
    {
      if (ModelState.IsValid)
      {
        pilot.Id = Guid.NewGuid();
        _context.Add(pilot);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(pilot);
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var pilot = await _context.Pilots.FindAsync(id);
      if (pilot == null)
      {
        return NotFound();
      }
      return View(pilot);
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,BirthDate,Genre")] Pilot pilot)
    {
      if (id != pilot.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(pilot);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!PilotExists(pilot.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(pilot);
    }

    [Roles(DefaultRoles.Max)]
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var pilot = await _context.Pilots
          .FirstOrDefaultAsync(m => m.Id == id);
      if (pilot == null)
      {
        return NotFound();
      }

      return View(pilot);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [Roles(DefaultRoles.Max)]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var pilot = await _context.Pilots.FindAsync(id);
      _context.Pilots.Remove(pilot);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool PilotExists(Guid id)
    {
      return _context.Pilots.Any(e => e.Id == id);
    }
  }
}
