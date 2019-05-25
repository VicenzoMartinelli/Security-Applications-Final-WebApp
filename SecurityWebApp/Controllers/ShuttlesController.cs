﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SecurityWebApp.Data;
using SecurityWebApp.Data.Model;

namespace SecurityWebApp.Controllers
{
  public class ShuttlesController : Controller
  {
    private readonly ApplicationDbContext _context;

    public ShuttlesController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Shuttles
    public async Task<IActionResult> Index()
    {
      return View(await _context.Shuttles.ToListAsync());
    }

    // GET: Shuttles/Details/5
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

    // GET: Shuttles/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: Shuttles/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Model,Producer,Value,Type")] Shuttle shuttle)
    {
      if (ModelState.IsValid)
      {
        shuttle.Id = Guid.NewGuid();
        _context.Add(shuttle);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
      }
      return View(shuttle);
    }

    // GET: Shuttles/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var shuttle = await _context.Shuttles.FindAsync(id);
      if (shuttle == null)
      {
        return NotFound();
      }
      return View(shuttle);
    }

    // POST: Shuttles/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Id,Model,Producer,Value,Type")] Shuttle shuttle)
    {
      if (id != shuttle.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(shuttle);
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

    // GET: Shuttles/Delete/5
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

    // POST: Shuttles/Delete/5
    [HttpPost, ActionName("Delete")]
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