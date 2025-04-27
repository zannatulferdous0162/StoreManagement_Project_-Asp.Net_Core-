using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;

namespace StoreManagement_Project.Controllers
{
    public class LocationComponentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationComponentController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var locationComponents = await _context.LocationComponents
                .Include(l => l.Aisle)
                .Include(l => l.Zone)
                .Include(l => l.Rack)
                .Include(l => l.Shelf)
                .Include(l => l.Bin)
                .Include(l => l.Warehouse)
                .ToListAsync();

            return View(locationComponents);  // Pass the list of location components to the view
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new LocationComponentViewModel();
            PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationComponentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var locationComponent = new LocationComponent
                {
                    AisleId = model.AisleId,
                    ZoneId = model.ZoneId,
                    RackId = model.RackId,
                    ShelfId = model.ShelfId,
                    BinId = model.BinId,
                    WarehouseId = model.WarehouseId
                };

                _context.LocationComponents.Add(locationComponent);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect to list page after creation
            }

            PopulateDropdowns(model); // Refill dropdowns if there is an error
            return View(model);
        }

        private async Task PopulateDropdowns(LocationComponentViewModel model)
        {
            model.Aisles = new SelectList(await _context.Aisles.ToListAsync(), "AisleId", "AisleName", model.AisleId);
            model.Zones = new SelectList(await _context.Zones.ToListAsync(), "ZoneId", "ZoneName", model.ZoneId);
            model.Racks = new SelectList(await _context.Racks.ToListAsync(), "RackId", "RackName", model.RackId);
            model.Shelves = new SelectList(await _context.shelves.ToListAsync(),
                                           "ShelfId",
                                           "ShelfName",
                                           model.ShelfId);
            model.Bins = new SelectList(await _context.Bins.ToListAsync(), "BinId", "BinName", model.BinId);
            model.Warehouses = new SelectList(await _context.Warehouses.ToListAsync(), "WarehouseId", "WarehouseName", model.WarehouseId);
        }
        // Get Aisles based on WarehouseId
        public JsonResult GetAisles(int warehouseId)
        {
            var aisles = _context.Aisles
                                 .Where(a => a.WarehouseId == warehouseId)
                                 .Select(a => new { value = a.AisleId, text = a.AisleName })
                                 .ToList();
            return Json(aisles);
        }

        // Get Zones based on WarehouseId
        public JsonResult GetZones(int warehouseId)
        {
            var zones = _context.Zones
                                .Where(z => z.WarehouseId == warehouseId)
                                .Select(z => new { value = z.ZoneId, text = z.ZoneName })
                                .ToList();
            return Json(zones);
        }

        // Get Racks based on WarehouseId
        public JsonResult GetRacks(int warehouseId)
        {
            var racks = _context.Racks
                                .Where(r => r.WarehouseId == warehouseId)
                                .Select(r => new { value = r.RackId, text = r.RackName })
                                .ToList();
            return Json(racks);
        }

        // Get Shelves based on WarehouseId
        public JsonResult GetShelves(int warehouseId)
        {
            var shelves = _context.shelves
                                  .Where(s => s.WarehouseId == warehouseId)
                                  .Select(s => new { value = s.ShelfId, text = s.ShelfName })
                                  .ToList();
            return Json(shelves);
        }

        // Get Bins based on WarehouseId
        public JsonResult GetBins(int warehouseId)
        {
            var bins = _context.Bins
                               .Where(b => b.WarehouseId == warehouseId)
                               .Select(b => new { value = b.BinId, text = b.BinName })
                               .ToList();
            return Json(bins);
        }

    }
}

