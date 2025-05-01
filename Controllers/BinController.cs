using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;

namespace StoreManagement_Project.Controllers
{
    public class AisleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AisleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Aisle
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aisles.Include(b => b.Warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Aisle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Aisle = await _context.Aisles
                .Include(b => b.Warehouse)
                .FirstOrDefaultAsync(m => m.AisleId == id);
            if (Aisle == null)
            {
                return NotFound();
            }

            return View(Aisle);
        }

        // GET: Aisle/Create
        public IActionResult Create()
        {
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name");
            return View();
        }

        // POST: Aisle/Create
        // To protect from overposting attacks, enable the specific properties you want to Aisled to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Aisled("AisleId,AisleName,Remarks,WarehouseId")] Aisle Aisle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Aisle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", Aisle.WarehouseId);
            return View(Aisle);
        }

        // GET: Aisle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Aisle = await _context.Aisles.FindAsync(id);
            if (Aisle == null)
            {
                return NotFound();
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", Aisle.WarehouseId);
            return View(Aisle);
        }

        // POST: Aisle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to Aisled to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Aisled("AisleId,AisleName,Remarks,WarehouseId")] Aisle Aisle)
        {
            if (id != Aisle.AisleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Aisle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AisleExists(Aisle.AisleId))
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
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", Aisle.WarehouseId);
            return View(Aisle);
        }

        // GET: Aisle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Aisle = await _context.Aisles
                .Include(b => b.Warehouse)
                .FirstOrDefaultAsync(m => m.AisleId == id);
            if (Aisle == null)
            {
                return NotFound();
            }

            return View(Aisle);
        }

        // POST: Aisle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Aisle = await _context.Aisles.FindAsync(id);
            if (Aisle != null)
            {
                _context.Aisles.Remove(Aisle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AisleExists(int id)
        {
            return _context.Aisles.Any(e => e.AisleId == id);
        }
    }
}
