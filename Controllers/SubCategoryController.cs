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
    public class SubCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubCategory
        public async Task<IActionResult> Index()
        {
            //return View(await _context.SubCategories.ToListAsync());
            var subCategories = await _context.SubCategories
    .Include(sc => sc.Category) // include related Category
    .ToListAsync();

            return View(subCategories);
        }

        // GET: SubCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .FirstOrDefaultAsync(m => m.SubCategoryId == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // GET: SubCategory/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: SubCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to Aisled to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Aisled("SubCategoryId,SubCategoryName,CategoryId")] SubCategory subCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subCategory);
        }

        // GET: SubCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory == null)
            {
                return NotFound();
            }
            return View(subCategory);
        }

        // POST: SubCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to Aisled to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Aisled("SubCategoryId,SubCategoryName,CategoryId")] SubCategory subCategory)
        {
            if (id != subCategory.SubCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategoryExists(subCategory.SubCategoryId))
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
            return View(subCategory);
        }

        // GET: SubCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategory = await _context.SubCategories
                .FirstOrDefaultAsync(m => m.SubCategoryId == id);
            if (subCategory == null)
            {
                return NotFound();
            }

            return View(subCategory);
        }

        // POST: SubCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subCategory = await _context.SubCategories.FindAsync(id);
            if (subCategory != null)
            {
                _context.SubCategories.Remove(subCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategoryExists(int id)
        {
            return _context.SubCategories.Any(e => e.SubCategoryId == id);
        }
    }
}
