//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using StoreManagement_Project.Data;
//using StoreManagement_Project.Models;

//namespace StoreManagement_Project.Controllers
//{
//    public class StockController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public StockController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: Stock
//        public async Task<IActionResult> Index()
//        {
//            var applicationDbContext = _context.Stocks.Include(s => s.Item).Include(s => s.Unit);
//            return View(await applicationDbContext.ToListAsync());
//        }

//        // GET: Stock/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var stock = await _context.Stocks
//                .Include(s => s.Item)
//                .Include(s => s.Unit)
//                .FirstOrDefaultAsync(m => m.StockId == id);
//            if (stock == null)
//            {
//                return NotFound();
//            }

//            return View(stock);
//        }
//        public IActionResult Create()
//        {
//            ViewData["Items"] = _context.Items
//                .Select(i => new {
//                    i.ItemId,
//                    i.ItemName,
//                    i.UnitId
//                }).ToList();

//            ViewData["Units"] = new SelectList(_context.Units, "UnitId", "NameOfUnit");
//            return View();
//        }


//        // POST: Stock/Create
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("StockId,ItemId,UnitId,Quantity")] Stock stock)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(stock);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", stock.ItemId);
//            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "NameOfUnit", stock.UnitId);
//            return View(stock);
//        }

//        // GET: Stock/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var stock = await _context.Stocks.FindAsync(id);
//            if (stock == null)
//            {
//                return NotFound();
//            }
//            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", stock.ItemId);
//            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "NameOfUnit", stock.UnitId);
//            return View(stock);
//        }

//        // POST: Stock/Edit/5
//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("StockId,ItemId,UnitId,Quantity")] Stock stock)
//        {
//            if (id != stock.StockId)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(stock);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!StockExists(stock.StockId))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", stock.ItemId);
//            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "NameOfUnit", stock.UnitId);
//            return View(stock);
//        }

//        // GET: Stock/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }

//            var stock = await _context.Stocks
//                .Include(s => s.Item)
//                .Include(s => s.Unit)
//                .FirstOrDefaultAsync(m => m.StockId == id);
//            if (stock == null)
//            {
//                return NotFound();
//            }

//            return View(stock);
//        }

//        // POST: Stock/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var stock = await _context.Stocks.FindAsync(id);
//            if (stock != null)
//            {
//                _context.Stocks.Remove(stock);
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }

//        private bool StockExists(int id)
//        {
//            return _context.Stocks.Any(e => e.StockId == id);
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;

namespace StoreManagement_Project.Controllers
{
    public class StockController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stocks
                .Include(s => s.Item)
                .Include(s => s.Unit)
                .Include(s => s.LocationComponent)
                .ThenInclude(l => l.Warehouse);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stock/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var stock = await _context.Stocks
                .Include(s => s.Item)
                .Include(s => s.Unit)
                .Include(s => s.LocationComponent)
                .ThenInclude(l => l.Warehouse)
                .FirstOrDefaultAsync(m => m.StockId == id);

            if (stock == null) return NotFound();
            return View(stock);
        }

        // GET: Stock/Create
        //public IActionResult Create()
        //{
        //    ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName");
        //    ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "NameOfUnit");
        //    ViewData["LocationComponentId"] = new SelectList(
        //        _context.LocationComponents
        //                .Include(l => l.Warehouse)
        //                .Select(l => new {
        //                    l.LocationComponentId,
        //                    Display = $"{l.Warehouse.Name} – {l.Location}"
        //                }),
        //        "LocationComponentId",
        //        "Display"
        //    );
        //    return View();
        //}

        // GET: Stock/Create
        public IActionResult Create()
        {
            // Ensure the items list is correctly populated
            var items = _context.Items
                .Select(i => new {
                    i.ItemId,
                    i.ItemName,
                    i.UnitId
                }).ToList();

            if (items == null || !items.Any())
            {
                // Handle the case where there are no items in the database
                ViewData["Items"] = new List<dynamic>();
            }
            else
            {
                ViewData["Items"] = items;
            }

            // Similarly for locations, make sure you're populating correctly
            var locs = _context.LocationComponents
                .Include(l => l.Warehouse)
                .Select(l => new {
                    l.LocationComponentId,
                    Display = $"{l.Warehouse.Name} – {l.Location}"
                })
                .ToList();

            if (locs == null || !locs.Any())
            {
                // Handle the case where there are no locations
                ViewData["LocationComponentId"] = new SelectList(new List<dynamic>());
            }
            else
            {
                ViewData["LocationComponentId"] = new SelectList(locs, "LocationComponentId", "Display");
            }

            // Add Units, assuming this is similarly populated:
            ViewData["Units"] = new SelectList(_context.Units, "UnitId", "NameOfUnit");

            return View();
        }


        // POST: Stock/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockId,ItemId,UnitId,Quantity,LocationComponentId")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", stock.ItemId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "NameOfUnit", stock.UnitId);
            ViewData["LocationComponentId"] = new SelectList(
                _context.LocationComponents.Include(l => l.Warehouse)
                           .Select(l => new {
                               l.LocationComponentId,
                               Display = $"{l.Warehouse.Name} – {l.Location}"
                           }),
                "LocationComponentId",
                "Display",
                stock.LocationComponentId
            );
            return View(stock);
        }

        // GET: Stock/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null) return NotFound();

            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", stock.ItemId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "NameOfUnit", stock.UnitId);
            ViewData["LocationComponentId"] = new SelectList(
                _context.LocationComponents.Include(l => l.Warehouse)
                           .Select(l => new {
                               l.LocationComponentId,
                               Display = $"{l.Warehouse.Name} – {l.Location}"
                           }),
                "LocationComponentId",
                "Display",
                stock.LocationComponentId
            );
            return View(stock);
        }

        // POST: Stock/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockId,ItemId,UnitId,Quantity,LocationComponentId")] Stock stock)
        {
            if (id != stock.StockId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Stocks.Any(e => e.StockId == stock.StockId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ItemId"] = new SelectList(_context.Items, "ItemId", "ItemName", stock.ItemId);
            ViewData["UnitId"] = new SelectList(_context.Units, "UnitId", "NameOfUnit", stock.UnitId);
            ViewData["LocationComponentId"] = new SelectList(
                _context.LocationComponents.Include(l => l.Warehouse)
                           .Select(l => new {
                               l.LocationComponentId,
                               Display = $"{l.Warehouse.Name} – {l.Location}"
                           }),
                "LocationComponentId",
                "Display",
                stock.LocationComponentId
            );
            return View(stock);
        }

        // GET: Stock/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var stock = await _context.Stocks
                .Include(s => s.Item)
                .Include(s => s.Unit)
                .Include(s => s.LocationComponent)
                .ThenInclude(l => l.Warehouse)
                .FirstOrDefaultAsync(m => m.StockId == id);
            if (stock == null) return NotFound();
            return View(stock);
        }

        // POST: Stock/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.StockId == id);
        }
    }
}

