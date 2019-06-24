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
using SecurityWebApp.Models;

namespace SecurityWebApp.Controllers
{
  [Authorize]
  public class ShuttlesController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ShuttlesController(ApplicationDbContext context)
    {
      _context = context;
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Index()
    {
      return View(await _context.Shuttles.ToListAsync());
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Details(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var shuttle = await _context.Shuttles
          .FirstOrDefaultAsync(m => m.Id == id);
      if (shuttle == null)
      {
        return NotFound();
      }

      return View(shuttle);
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Create()
    {
      var pilots     = new SelectList(await _context.Pilots.ToListAsync(), "Id", "Name");
      ViewBag.Pilots = pilots;

      return View();
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Model,Producer,Value,Type,PilotId")] ShuttleViewModel shuttle)
    {
      if (ModelState.IsValid)
      {
        var model = new Shuttle()
        {
          Id       = Guid.NewGuid(),
          Model    = shuttle.Model,
          Pilot    = shuttle.PilotId.HasValue ? _context.Pilots.Find(shuttle.PilotId.Value) : null,
          Producer = shuttle.Producer,
          Type     = shuttle.Type,
           Value   = shuttle.Value
        };

        _context.Add(model);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
      }
      return View(shuttle);
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var shuttle = await _context.Shuttles.Include(x => x.Pilot).Where(x => x.Id == id).SingleOrDefaultAsync();

      if (shuttle == null)
      {
        return NotFound();
      }

      var pilots = new SelectList(_context.Pilots, "Id", "Name", shuttle.Pilot?.Id);
      ViewBag.Pilots = pilots;

      return View(shuttle);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Model,Producer,Value,Type,PilotId")] ShuttleViewModel shuttle)
    {
      if (id != shuttle.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          var model = await _context.Shuttles.FindAsync(shuttle.Id);

          model.Model    = shuttle.Model;
          model.Pilot    = shuttle.PilotId.HasValue ? _context.Pilots.Find(shuttle.PilotId.Value) : null;
          model.Producer = shuttle.Producer;
          model.Type     = shuttle.Type;
          model.Value    = shuttle.Value;

          _context.Update(model);

          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!ShuttleExists(shuttle.Id))
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
      return View(shuttle);
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var shuttle = await _context.Shuttles
          .FirstOrDefaultAsync(m => m.Id == id);
      if (shuttle == null)
      {
        return NotFound();
      }

      return View(shuttle);
    }

    [HttpPost, ActionName("Delete")]
    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var shuttle = await _context.Shuttles.FindAsync(id);

      _context.Shuttles.Remove(shuttle);

      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool ShuttleExists(Guid id)
    {
      return _context.Shuttles.Any(e => e.Id == id);
    }
  }
}
