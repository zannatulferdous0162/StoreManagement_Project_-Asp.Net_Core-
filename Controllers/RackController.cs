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
    public class RackController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RackController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rack
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Racks.Include(r => r.Warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Rack/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(m => m.RackId == id);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // GET: Rack/Create
        public IActionResult Create()
        {
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name");
            return View();
        }

        // POST: Rack/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RackId,RackName,Remarks,WarehouseId")] Rack rack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", rack.WarehouseId);
            return View(rack);
        }

        // GET: Rack/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks.FindAsync(id);
            if (rack == null)
            {
                return NotFound();
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", rack.WarehouseId);
            return View(rack);
        }

        // POST: Rack/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RackId,RackName,Remarks,WarehouseId")] Rack rack)
        {
            if (id != rack.RackId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RackExists(rack.RackId))
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
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", rack.WarehouseId);
            return View(rack);
        }

        // GET: Rack/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rack = await _context.Racks
                .Include(r => r.Warehouse)
                .FirstOrDefaultAsync(m => m.RackId == id);
            if (rack == null)
            {
                return NotFound();
            }

            return View(rack);
        }

        // POST: Rack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rack = await _context.Racks.FindAsync(id);
            if (rack != null)
            {
                _context.Racks.Remove(rack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RackExists(int id)
        {
            return _context.Racks.Any(e => e.RackId == id);
        }
    }
}
