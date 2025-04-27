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
    public class PackSizeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PackSizeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PackSize
        public async Task<IActionResult> Index()
        {
            return View(await _context.PackSizes.ToListAsync());
        }

        // GET: PackSize/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packSize = await _context.PackSizes
                .FirstOrDefaultAsync(m => m.PackSizeId == id);
            if (packSize == null)
            {
                return NotFound();
            }

            return View(packSize);
        }

        // GET: PackSize/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PackSize/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PackSizeId,Size")] PackSize packSize)
        {
            if (ModelState.IsValid)
            {
                _context.Add(packSize);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(packSize);
        }

        // GET: PackSize/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packSize = await _context.PackSizes.FindAsync(id);
            if (packSize == null)
            {
                return NotFound();
            }
            return View(packSize);
        }

        // POST: PackSize/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PackSizeId,Size")] PackSize packSize)
        {
            if (id != packSize.PackSizeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(packSize);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PackSizeExists(packSize.PackSizeId))
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
            return View(packSize);
        }

        // GET: PackSize/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var packSize = await _context.PackSizes
                .FirstOrDefaultAsync(m => m.PackSizeId == id);
            if (packSize == null)
            {
                return NotFound();
            }

            return View(packSize);
        }

        // POST: PackSize/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var packSize = await _context.PackSizes.FindAsync(id);
            if (packSize != null)
            {
                _context.PackSizes.Remove(packSize);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PackSizeExists(int id)
        {
            return _context.PackSizes.Any(e => e.PackSizeId == id);
        }
    }
}
