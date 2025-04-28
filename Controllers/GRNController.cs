using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;
using StoreManagement_Project.ViewModels;

namespace StoreManagement_Project.Controllers
{
    public class GRNController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GRNController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            var model = new GRNViewModel
            {
                Warehouses = _context.Warehouses.ToList(),
                ReceivedDate = DateTime.Now,
                Suppliers = _context.Suppliers.Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.SupplierName
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GRNViewModel model)
        {
            if (model.GRNItems == null || !model.GRNItems.Any())
            {
                ModelState.AddModelError("", "At least one item must be included.");
            }

            // Pre-generate GRNNumber if PurchaseOrder is selected
            if (model.PurchaseOrderId > 0)
            {
                var po = await _context.PurchaseOrders
                    .FirstOrDefaultAsync(p => p.PurchaseOrderId == model.PurchaseOrderId);

                if (po != null)
                {
                    var count = await _context.GRNs
                        .CountAsync(g => g.GRNNumber.StartsWith($"GRN-{po.PONo}/"));
                    var serial = (count + 1).ToString("D2");
                    model.GRNNumber = $"GRN-{po.PONo}/{serial}"; // 🔥 assign here
                }
            }

            if (ModelState.IsValid)
            {
                var grn = new GRN
                {
                    GRNNumber = model.GRNNumber,
                    SupplierId = model.SupplierId,
                    PurchaseOrderId = model.PurchaseOrderId,
                    WarehouseId = model.WarehouseId,
                    ReceivedDate = model.ReceivedDate,
                    InvoiceDate = (DateTime)model.InvoiceDate,
                    InvoiceNo = model.InvoiceNo,
                    ReceivedBy = model.ReceivedBy,
                    GRNItems = new List<GRNItem>()
                };

                foreach (var item in model.GRNItems)
                {
                    var totalReceived = await _context.GRNs
                        .Where(g => g.PurchaseOrderId == model.PurchaseOrderId)
                        .SelectMany(g => g.GRNItems)
                        .Where(i => i.ItemId == item.ItemId)
                        .SumAsync(i => (decimal?)i.QuantityReceived) ?? 0;

                    var remainingQty = item.Quantity - totalReceived - item.QuantityReceived;

                    grn.GRNItems.Add(new GRNItem
                    {
                        ItemId = item.ItemId,
                        LocationComponentId = item.LocationComponentId,
                        ItemName = item.ItemName,
                        Quantity = item.Quantity,
                        UnitName = item.UnitName,
                        QuantityReceived = item.QuantityReceived,
                        RemainingQuantity = remainingQty < 0 ? 0 : remainingQty,
                        Inspection = item.Inspection
                    });

                    var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ItemId == item.ItemId);
                    if (stock != null)
                    {
                        stock.Quantity += item.QuantityReceived;
                        _context.Stocks.Update(stock);
                    }
                    else
                    {
                        _context.Stocks.Add(new Stock
                        {
                            ItemId = item.ItemId,
                            Quantity = item.QuantityReceived
                        });
                    }
                }

                _context.GRNs.Add(grn);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // Reload dropdowns
            model.Suppliers = _context.Suppliers.Select(s => new SelectListItem
            {
                Value = s.SupplierId.ToString(),
                Text = s.SupplierName
            }).ToList();

            if (model.SupplierId != 0)
            {
                model.PurchaseOrders = _context.PurchaseOrders
                    .Where(po => po.SupplierId == model.SupplierId)
                    .Select(po => new SelectListItem
                    {
                        Value = po.PurchaseOrderId.ToString(),
                        Text = po.PONo
                    }).ToList();
            }

            return View(model);
        }


        // AJAX: Get Purchase Orders by Supplier
        public IActionResult GetPurchaseOrdersBySupplier(int supplierId)
        {
            var pos = _context.PurchaseOrders
                .Where(po => po.SupplierId == supplierId)
                .Select(po => new { po.PurchaseOrderId, po.PONo })
                .ToList();

            return Json(pos);
        }

        // AJAX: Get PO Items + RemainingQuantity
        public IActionResult GetPOItems(int purchaseOrderId)
        {
            var items = _context.PurchaseOrderItems
                .Include(poi => poi.Item)
                .Include(poi => poi.Unit)
                .Where(poi => poi.PurchaseOrderId == purchaseOrderId)
                .ToList()
                .Select(poi =>
                {
                    var totalReceived = _context.GRNs
                        .Where(g => g.PurchaseOrderId == purchaseOrderId)
                        .SelectMany(g => g.GRNItems)
                        .Where(i => i.ItemId == poi.ItemId)
                        .Sum(i => (decimal?)i.QuantityReceived) ?? 0;

                    var remainingQuantity = poi.Quantity - totalReceived;

                    return new
                    {
                        poi.ItemId,
                        ItemName = poi.Item.ItemName,
                        poi.Quantity,
                        UnitName = poi.Unit.NameOfUnit,
                        QuantityReceived = totalReceived,
                        RemainingQuantity = remainingQuantity
                    };
                })
                .Where(item => item.RemainingQuantity > 0) // Filter here
                .ToList();

            return Json(items);
        }
        // When PO is selected, get associated Warehouse
        [HttpGet]
        public IActionResult GetWarehouseByPO(int purchaseOrderId)
        {
            var warehouse = _context.PurchaseOrders
                .Where(p => p.PurchaseOrderId == purchaseOrderId)
                .Select(p => new {
                    p.WarehouseId,
                    WarehouseName = p.Warehouse.Name
                })
                .FirstOrDefault();

            return Json(warehouse);
        }

        // When Warehouse is selected, get LocationComponents under it
        [HttpGet]
        public IActionResult GetLocationComponentsByWarehouse(int warehouseId)
        {
            var locations = _context.LocationComponents
                .Where(l => l.WarehouseId == warehouseId) // top-level
                .Select(l => new {
                    l.LocationComponentId,
                    l.Location
                }).ToList();

            return Json(locations);
        }


        public async Task<IActionResult> Index()
        {
            var grns = await _context.GRNs
                .Include(g => g.Supplier)
                .Include(g => g.PurchaseOrder)
                .Include(g => g.Warehouse)
                .OrderByDescending(g => g.ReceivedDate)
                .ToListAsync();

            return View(grns);
        }
        [HttpGet]
        public async Task<IActionResult> GenerateGRNNumber(int purchaseOrderId)
        {
            var po = await _context.PurchaseOrders.FirstOrDefaultAsync(p => p.PurchaseOrderId == purchaseOrderId);
            if (po == null)
                return Json("");

            var count = await _context.GRNs
                .CountAsync(g => g.GRNNumber.StartsWith($"GRN-{po.PONo}/"));
            var serial = (count + 1).ToString("D2");
            var grnNumber = $"GRN-{po.PONo}/{serial}";

            return Json(grnNumber);
        }

        public async Task<IActionResult> Details(int id)
        {
            var grn = await _context.GRNs
                .Include(g => g.Supplier)
                .Include(g => g.PurchaseOrder)
                .Include(g => g.Warehouse)
                .Include(g => g.GRNItems)
                    .ThenInclude(gi => gi.Item)
                .FirstOrDefaultAsync(g => g.GRNId == id);

            if (grn == null)
            {
                return NotFound();
            }

            return View(grn);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var grn = await _context.GRNs
                .Include(g => g.Supplier)
                .Include(g => g.PurchaseOrder)
                .FirstOrDefaultAsync(g => g.GRNId == id);

            if (grn == null)
                return NotFound();

            return View(grn);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grn = await _context.GRNs
                .Include(g => g.GRNItems)
                .FirstOrDefaultAsync(g => g.GRNId == id);

            if (grn == null)
                return NotFound();

            foreach (var item in grn.GRNItems)
            {
                var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ItemId == item.ItemId);
                if (stock != null)
                {
                    stock.Quantity -= item.QuantityReceived;

                    // ❗ Just in case, prevent negative stock
                    if (stock.Quantity < 0)
                        stock.Quantity = 0;

                    _context.Stocks.Update(stock);
                }
            }
            // Remove related GRNItems first
            if (grn.GRNItems != null && grn.GRNItems.Any())
            {
                _context.GRNItems.RemoveRange(grn.GRNItems);
            }

            _context.GRNs.Remove(grn);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var grn = await _context.GRNs
                .Include(g => g.GRNItems)
                .FirstOrDefaultAsync(g => g.GRNId == id);

            if (grn == null) return NotFound();

            var model = new GRNViewModel
            {
                GRNId = grn.GRNId,
                GRNNumber = grn.GRNNumber,
                SupplierId = grn.SupplierId,
                PurchaseOrderId = grn.PurchaseOrderId,
                InvoiceDate = grn.InvoiceDate,
                InvoiceNo = grn.InvoiceNo,
                ReceivedDate = grn.ReceivedDate,
                ReceivedBy = grn.ReceivedBy,
                GRNItems = grn.GRNItems.Select(i => new GRNItemViewModel
                {
                    ItemId = i.ItemId,
                    ItemName = i.ItemName,
                    Quantity = i.Quantity,
                    UnitName = i.UnitName,
                    QuantityReceived = i.QuantityReceived,
                    //RemainingQuantity = i.RemainingQuantity
                }).ToList(),
                Suppliers = _context.Suppliers.Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.SupplierName
                }).ToList(),
                PurchaseOrders = _context.PurchaseOrders
                    .Where(po => po.SupplierId == grn.SupplierId)
                    .Select(po => new SelectListItem
                    {
                        Value = po.PurchaseOrderId.ToString(),
                        Text = po.PONo
                    }).ToList()
            };

            return View(model);
        }
        //[HttpPost("Edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, GRNViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Suppliers = _context.Suppliers.Select(s => new SelectListItem
                {
                    Value = s.SupplierId.ToString(),
                    Text = s.SupplierName
                }).ToList();

                model.PurchaseOrders = _context.PurchaseOrders
                    .Where(po => po.SupplierId == model.SupplierId)
                    .Select(po => new SelectListItem
                    {
                        Value = po.PurchaseOrderId.ToString(),
                        Text = po.PONo
                    }).ToList();

                return View(model);
            }

            var existingGRN = await _context.GRNs
                .Include(g => g.GRNItems)
                .FirstOrDefaultAsync(g => g.GRNId == model.GRNId);

            if (existingGRN == null)
                return NotFound();

            // Update stock: rollback previous GRN quantities
            foreach (var oldItem in existingGRN.GRNItems)
            {
                var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ItemId == oldItem.ItemId);
                if (stock != null)
                {
                    stock.Quantity -= oldItem.QuantityReceived;
                    if (stock.Quantity < 0) stock.Quantity = 0;
                    _context.Stocks.Update(stock);
                }
            }

            // Remove old GRN items
            _context.GRNItems.RemoveRange(existingGRN.GRNItems);

            // Update GRN
            existingGRN.SupplierId = model.SupplierId;
            existingGRN.PurchaseOrderId = model.PurchaseOrderId;
            existingGRN.InvoiceDate = model.InvoiceDate ?? DateTime.Now;
            existingGRN.InvoiceNo = model.InvoiceNo;
            existingGRN.ReceivedDate = model.ReceivedDate;
            existingGRN.ReceivedBy = model.ReceivedBy;

            var newItems = new List<GRNItem>();
            foreach (var item in model.GRNItems)
            {
                var totalReceived = await _context.GRNItems
                    .Where(g => g.GRN.PurchaseOrderId == model.PurchaseOrderId && g.ItemId == item.ItemId && g.GRNId != model.GRNId)
                    .SumAsync(i => (decimal?)i.QuantityReceived) ?? 0;

                var remainingQty = item.Quantity - totalReceived - item.QuantityReceived;

                newItems.Add(new GRNItem
                {
                    GRNId = existingGRN.GRNId,
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Quantity = item.Quantity,
                    UnitName = item.UnitName,
                    QuantityReceived = item.QuantityReceived,
                    RemainingQuantity = remainingQty < 0 ? 0 : remainingQty
                });

                var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.ItemId == item.ItemId);
                if (stock != null)
                {
                    stock.Quantity += item.QuantityReceived;
                    _context.Stocks.Update(stock);
                }
                else
                {
                    _context.Stocks.Add(new Stock
                    {
                        ItemId = item.ItemId,
                        Quantity = item.QuantityReceived
                    });
                }
            }

            existingGRN.GRNItems = newItems;

            _context.GRNs.Update(existingGRN);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
