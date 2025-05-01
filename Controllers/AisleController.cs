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
            var applicationDbContext = _context.Aisles.Include(a => a.Warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Aisle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aisle = await _context.Aisles
                .Include(a => a.Warehouse)
                .FirstOrDefaultAsync(m => m.AisleId == id);
            if (aisle == null)
            {
                return NotFound();
            }

            return View(aisle);
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Aisled("AisleId,AisleName,Remarks,WarehouseId")] Aisle aisle)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(aisle);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", aisle.WarehouseId);
        //    return View(aisle);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Aisled("AisleId,AisleName,Remarks,WarehouseId")] Aisle aisle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aisle);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Aisle created successfully!";

                // Redirect to same page to trigger SweetAlert2
                return RedirectToAction(nameof(Create));
            }

            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", aisle.WarehouseId);
            return View(aisle);
        }


        // GET: Aisle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aisle = await _context.Aisles.FindAsync(id);
            if (aisle == null)
            {
                return NotFound();
            }
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", aisle.WarehouseId);
            TempData["SuccessMessage"] = "Aisle updated successfully!";
            //return RedirectToAction("Index");

            return View(aisle);
        }

        // POST: Aisle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to Aisled to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Aisled("AisleId,AisleName,Remarks,WarehouseId")] Aisle aisle)
        {
            if (id != aisle.AisleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aisle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AisleExists(aisle.AisleId))
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
            ViewData["WarehouseId"] = new SelectList(_context.Warehouses, "WarehouseId", "Name", aisle.WarehouseId);
            return View(aisle);
        }

        // GET: Aisle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aisle = await _context.Aisles
                .Include(a => a.Warehouse)
                .FirstOrDefaultAsync(m => m.AisleId == id);
            if (aisle == null)
            {
                return NotFound();
            }
            TempData["DeleteSuccess"] = true;
            return View(aisle);
        }

        // POST: Aisle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aisle = await _context.Aisles.FindAsync(id);
            if (aisle != null)
            {
                _context.Aisles.Remove(aisle);
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
