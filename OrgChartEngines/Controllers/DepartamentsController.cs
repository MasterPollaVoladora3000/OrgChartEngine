﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrgChartEngines.Data;
using OrgChartEngines.Models.OrgChart;

namespace OrgChartEngines.Controllers
{
    public class DepartamentsController : Controller
    {
        private readonly Engines_PCContext _context;

        public DepartamentsController(Engines_PCContext context)
        {
            _context = context;
        }

        // GET: Departaments
        public async Task<IActionResult> Index()
        {
              return _context.Departaments != null ? 
                          View(await _context.Departaments.ToListAsync()) :
                          Problem("Entity set 'Weld_PCContext.Departaments'  is null.");
        }

        // GET: Departaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Departaments == null)
            {
                return NotFound();
            }

            var departament = await _context.Departaments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // GET: Departaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DepartamentName")] Departament departament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departament);
        }

        // GET: Departaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Departaments == null)
            {
                return NotFound();
            }

            var departament = await _context.Departaments.FindAsync(id);
            if (departament == null)
            {
                return NotFound();
            }
            return View(departament);
        }

        // POST: Departaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartamentName")] Departament departament)
        {
            if (id != departament.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartamentExists(departament.Id))
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
            return View(departament);
        }

        // GET: Departaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Departaments == null)
            {
                return NotFound();
            }

            var departament = await _context.Departaments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (departament == null)
            {
                return NotFound();
            }

            return View(departament);
        }

        // POST: Departaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Departaments == null)
            {
                return Problem("Entity set 'Weld_PCContext.Departaments'  is null.");
            }
            var departament = await _context.Departaments.FindAsync(id);
            if (departament != null)
            {
                _context.Departaments.Remove(departament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartamentExists(int id)
        {
          return (_context.Departaments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost]
        public JsonResult LoadDepartment()
        {
            //_context.Configuration.ProxyCreationEnabled = false;

            var data = _context.Departaments.ToList();
            return Json(data);
        }
    }
}
