using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;

namespace StoreManagement_Project.Controllers
{
    public class UnitSetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitSetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UnitSet
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnitSets.ToListAsync());
        }

        // GET: UnitSet/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitSet = await _context.UnitSets
                .Include(us => us.Units) // 👈 Includes related Units
                .FirstOrDefaultAsync(m => m.UnitSetId == id);

            if (unitSet == null)
            {
                return NotFound();
            }

            return View(unitSet);
        }


        // GET: UnitSet/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnitSet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UnitSetId,NameOfUnitSet,Description,Remarks")] UnitSet unitSet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unitSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitSet);
        }

        // GET: UnitSet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitSet = await _context.UnitSets.FindAsync(id);
            if (unitSet == null)
            {
                return NotFound();
            }
            return View(unitSet);
        }

        // POST: UnitSet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UnitSetId,NameOfUnitSet,Description,Remarks")] UnitSet unitSet)
        {
            if (id != unitSet.UnitSetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unitSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnitSetExists(unitSet.UnitSetId))
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
            return View(unitSet);
        }

        // GET: UnitSet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unitSet = await _context.UnitSets
                .FirstOrDefaultAsync(m => m.UnitSetId == id);
            if (unitSet == null)
            {
                return NotFound();
            }

            return View(unitSet);
        }

        // POST: UnitSet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unitSet = await _context.UnitSets.FindAsync(id);
            if (unitSet != null)
            {
                _context.UnitSets.Remove(unitSet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnitSetExists(int id)
        {
            return _context.UnitSets.Any(e => e.UnitSetId == id);
        }
    }
}
