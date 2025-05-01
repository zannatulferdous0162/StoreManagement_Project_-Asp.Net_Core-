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
                .Include(l => l.Aisle)
                .Include(l => l.Warehouse)
                .ToListAsync();

            return View(locationComponents);  // Pass the list of location components to the view
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new LocationComponentViewModel
            {
                Warehouses = _context.Warehouses
                    .Select(w => new SelectListItem { Value = w.WarehouseId.ToString(), Text = w.Name })
                    .ToList(),

                Aisles = new List<SelectListItem>(),   // Empty initially
                Zones = new List<SelectListItem>(),
                Racks = new List<SelectListItem>(),
                Shelves = new List<SelectListItem>(),
                Aisles = new List<SelectListItem>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationComponentViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Generate the location string
                var locationParts = new List<string>();

                var aisleText = _context.Aisles.Where(a => a.AisleId == model.AisleId).Select(a => a.AisleName).FirstOrDefault();
                var zoneText = _context.Zones.Where(z => z.ZoneId == model.ZoneId).Select(z => z.ZoneName).FirstOrDefault();
                var rackText = _context.Racks.Where(r => r.RackId == model.RackId).Select(r => r.RackName).FirstOrDefault();
                var shelfText = _context.shelves.Where(s => s.ShelfId == model.ShelfId).Select(s => s.ShelfName).FirstOrDefault();
                var AisleText = _context.Aisles.Where(b => b.AisleId == model.AisleId).Select(b => b.AisleName).FirstOrDefault();

                if (!string.IsNullOrEmpty(aisleText)) locationParts.Add($"Aisle {aisleText}");
                if (!string.IsNullOrEmpty(zoneText)) locationParts.Add($"Zone {zoneText}");
                if (!string.IsNullOrEmpty(rackText)) locationParts.Add($"Rack {rackText}");
                if (!string.IsNullOrEmpty(shelfText)) locationParts.Add($"Shelf {shelfText}");
                if (!string.IsNullOrEmpty(AisleText)) locationParts.Add($"Aisle {AisleText}");

                string fullLocation = string.Join(" > ", locationParts);

                // Create a new LocationComponent
                var locationComponent = new LocationComponent
                {
                    AisleId = model.AisleId,
                    ZoneId = model.ZoneId,
                    RackId = model.RackId,
                    ShelfId = model.ShelfId,
                    AisleId = model.AisleId,
                    WarehouseId = model.WarehouseId,
                    Location = fullLocation // Save the full location string
                };

                _context.LocationComponents.Add(locationComponent);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect to the list page after creation
            }

            // Re-populate dropdowns if model state is invalid
            model.Warehouses = _context.Warehouses
                .Select(w => new SelectListItem { Value = w.WarehouseId.ToString(), Text = w.Name })
                .ToList();

            return View(model);
        }


        // Get all Warehouses
        public JsonResult GetWarehouses()
        {
            var warehouses = _context.Warehouses
                                      .Select(w => new { value = w.WarehouseId, text = w.Name })
                                      .ToList();
            return Json(warehouses);
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

        // Get Aisles based on WarehouseId
        public JsonResult GetAisles(int warehouseId)
        {
            var Aisles = _context.Aisles
                               .Where(b => b.WarehouseId == warehouseId)
                               .Select(b => new { value = b.AisleId, text = b.AisleName })
                               .ToList();
            return Json(Aisles);
        }
        [HttpGet]
        public JsonResult CheckDuplicateLocation(string location)
        {
            var exists = _context.LocationComponents.Any(l => l.Location == location);
            return Json(!exists);
        }
        // GET: LocationComponent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationComponent = await _context.LocationComponents
                .Include(l => l.Aisle)
                .Include(l => l.Zone)
                .Include(l => l.Rack)
                .Include(l => l.Shelf)
                .Include(l => l.Aisle)
                .Include(l => l.Warehouse)
                .FirstOrDefaultAsync(m => m.LocationComponentId == id);

            if (locationComponent == null)
            {
                return NotFound();
            }

            return View(locationComponent);
        }
        // GET: LocationComponent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locationComponent = await _context.LocationComponents
                .Include(l => l.Aisle)
                .Include(l => l.Zone)
                .Include(l => l.Rack)
                .Include(l => l.Shelf)
                .Include(l => l.Aisle)
                .Include(l => l.Warehouse)
                .FirstOrDefaultAsync(m => m.LocationComponentId == id);

            if (locationComponent == null)
            {
                return NotFound();
            }

            return View(locationComponent);
        }

        // POST: LocationComponent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locationComponent = await _context.LocationComponents.FindAsync(id);
            _context.LocationComponents.Remove(locationComponent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index)); // Redirect to the list page after deletion
        }

    }
}

