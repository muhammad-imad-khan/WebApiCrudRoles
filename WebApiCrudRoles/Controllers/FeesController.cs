﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiCrudRoles.Models;

namespace WebApiCrudRoles.Controllers
{
    public class FeesController : Controller
    {
        private readonly FeesDbContext _context;

        public FeesController(FeesDbContext context)
        {
            _context = context;
        }

        // GET: Fees
        public async Task<IActionResult> Index()
        {
              return View(await _context.Fees.ToListAsync());
        }

        // GET: Fees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fees = await _context.Fees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fees == null)
            {
                return NotFound();
            }

            return View(fees);
        }

        // GET: Fees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Fee,FeeStatement,Due,Id,Name,Age,ClassSchedule,Courses")] Fees fees)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fees);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fees);
        }

        // GET: Fees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fees = await _context.Fees.FindAsync(id);
            if (fees == null)
            {
                return NotFound();
            }
            return View(fees);
        }

        // POST: Fees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Fee,FeeStatement,Due,Id,Name,Age,ClassSchedule,Courses")] Fees fees)
        {
            if (id != fees.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeesExists(fees.Id))
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
            return View(fees);
        }

        // GET: Fees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fees == null)
            {
                return NotFound();
            }

            var fees = await _context.Fees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fees == null)
            {
                return NotFound();
            }

            return View(fees);
        }

        // POST: Fees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fees == null)
            {
                return Problem("Entity set 'FeesDbContext.Fees'  is null.");
            }
            var fees = await _context.Fees.FindAsync(id);
            if (fees != null)
            {
                _context.Fees.Remove(fees);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeesExists(int id)
        {
          return _context.Fees.Any(e => e.Id == id);
        }
    }
}
