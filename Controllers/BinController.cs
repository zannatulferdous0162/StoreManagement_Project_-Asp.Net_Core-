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
    public class BinController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BinController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bin
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Bins.Include(b => b.Warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Bin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bin = await _context.Bins
                .Include(b => b.Warehouse)
                .FirstOrDefaultAsync(m => m.BinId == id);
            if (bin == null)
            {
                return NotFound();
            }

            return View(bin);
        }

        // GET: Bin/Create
        public IActionResult Create()
        {
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name");
            return View();
        }

        // POST: Bin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BinId,BinName,Remarks,WarehouseId")] Bin bin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", bin.WarehouseId);
            return View(bin);
        }

        // GET: Bin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bin = await _context.Bins.FindAsync(id);
            if (bin == null)
            {
                return NotFound();
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", bin.WarehouseId);
            return View(bin);
        }

        // POST: Bin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BinId,BinName,Remarks,WarehouseId")] Bin bin)
        {
            if (id != bin.BinId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BinExists(bin.BinId))
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
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", bin.WarehouseId);
            return View(bin);
        }

        // GET: Bin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bin = await _context.Bins
                .Include(b => b.Warehouse)
                .FirstOrDefaultAsync(m => m.BinId == id);
            if (bin == null)
            {
                return NotFound();
            }

            return View(bin);
        }

        // POST: Bin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bin = await _context.Bins.FindAsync(id);
            if (bin != null)
            {
                _context.Bins.Remove(bin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BinExists(int id)
        {
            return _context.Bins.Any(e => e.BinId == id);
        }
    }
}
