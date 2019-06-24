using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SecurityWebApp.Attributes;
using SecurityWebApp.Data;
using SecurityWebApp.Data.Model;

namespace SecurityWebApp.Controllers
{
  [Authorize]
  public class StarsController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly ILogger<StarsController> _logger;

    public StarsController(ApplicationDbContext context, ILogger<StarsController> logger)
    {
      _context = context;
      _logger  = logger;
    }

    public async Task<IActionResult> Index()
    {
      return View(await _context.Stars.ToListAsync());
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public IActionResult Create()
    {
      return View();
    }

    // POST: Stars/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Class")] Star star)
    {
      if (ModelState.IsValid)
      {
        star.Id = Guid.NewGuid();
        _context.Add(star);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(star);
    }

    
    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var star = await _context.Stars.FindAsync(id);
      if (star == null)
      {
        return NotFound();
      }
      return View(star);
    }

    // POST: Stars/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Class")] Star star)
    {
      if (id != star.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(star);
          await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!StarExists(star.Id))
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
      return View(star);
    }

    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    public async Task<IActionResult> Delete(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var star = await _context.Stars
          .FirstOrDefaultAsync(m => m.Id == id);
      if (star == null)
      {
        return NotFound();
      }

      return View(star);
    }

    [HttpPost, ActionName("Delete")]
    [Roles(DefaultRoles.Max, DefaultRoles.Med)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
      var star = await _context.Stars.FindAsync(id);
      _context.Stars.Remove(star);
      await _context.SaveChangesAsync();
      return RedirectToAction(nameof(Index));
    }

    private bool StarExists(Guid id)
    {
      return _context.Stars.Any(e => e.Id == id);
    }
  }
}
