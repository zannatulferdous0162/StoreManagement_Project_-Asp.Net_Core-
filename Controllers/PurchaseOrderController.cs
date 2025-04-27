using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;

namespace StoreManagement_Project.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PurchaseOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrder/Create
        public IActionResult Create()
        {
            ViewBag.Suppliers = _context.Suppliers.ToList();
            ViewBag.Items = _context.Items
                .Include(i => i.Unit)
                .ToList();
            ViewBag.Units = _context.Units.ToList();
            ViewBag.Warehouses = _context.Warehouses.ToList();
            ViewBag.Currencies = _context.Currencies.ToList();


            // ✅ Generate next PO Number
            int year = DateTime.Now.Year;
            int count = _context.PurchaseOrders.Count(p => p.PONo.StartsWith($"PO-{year}-"));
            string serial = (count + 1).ToString("D2");
            string nextPONo = $"PO-{year}-{serial}";

            ViewBag.NextPONo = nextPONo;

            return View();
        }       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PurchaseOrder purchaseOrder)
        {
            try
            {
                // 1. Manually bind the PurchaseOrderItems from the form
                var purchaseOrderItems = new List<PurchaseOrderItem>();
                var form = await Request.ReadFormAsync();

                // Get all item indices from the form
                var indices = form.Keys
                    .Where(k => k.StartsWith("PurchaseOrderItem["))
                    .Select(k => int.Parse(k.Split('[')[1].Split(']')[0]))
                    .Distinct()
                    .ToList();

                foreach (var index in indices)
                {
                    var itemId = form[$"PurchaseOrderItem[{index}].ItemId"].ToString();
                    var quantity = form[$"PurchaseOrderItem[{index}].Quantity"].ToString();
                    var price = form[$"PurchaseOrderItem[{index}].Price"].ToString();
                    var unitId = form[$"PurchaseOrderItem[{index}].UnitId"].ToString();

                    if (!string.IsNullOrEmpty(itemId) && int.TryParse(itemId, out var itemIdValue) &&
                        !string.IsNullOrEmpty(quantity) && int.TryParse(quantity, out var quantityValue) &&
                        !string.IsNullOrEmpty(price) && decimal.TryParse(price, out var priceValue) &&
                        !string.IsNullOrEmpty(unitId) && int.TryParse(unitId, out var unitIdValue))
                    {
                        purchaseOrderItems.Add(new PurchaseOrderItem
                        {
                            ItemId = itemIdValue,
                            Quantity = quantityValue,
                            Price = priceValue,
                            UnitId = unitIdValue
                        });
                    }
                }

                if (purchaseOrderItems.Count == 0)
                {
                    ModelState.AddModelError("", "At least one item is required");
                }

                if (ModelState.IsValid)
                {
                    // ✅ 2. Generate PO Number in format "PO-2025-01"
                    int year = DateTime.Now.Year;
                    int count = await _context.PurchaseOrders
                        .CountAsync(p => p.PONo.StartsWith($"PO-{year}-"));
                    string serial = (count + 1).ToString("D2");
                    purchaseOrder.PONo = $"PO-{year}-{serial}";

                    // ✅ 3. Add the items and save everything
                    purchaseOrder.PurchaseOrderItem = purchaseOrderItems;

                    _context.PurchaseOrders.Add(purchaseOrder);
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index), new { id = purchaseOrder.PurchaseOrderId });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error saving data: " + ex.Message);
            }

            // Reload dropdowns if validation fails
            ViewBag.Suppliers = _context.Suppliers.ToList();
            ViewBag.Items = _context.Items.Include(i => i.Unit).ToList();
            ViewBag.Units = _context.Units.ToList();
            ViewBag.Warehouses = _context.Warehouses.ToList();
            ViewBag.Currencies = _context.Currencies.ToList();


            return View(purchaseOrder);
        }


        // GET: PurchaseOrder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.PurchaseOrderItem)
                    .ThenInclude(item => item.Item)
                .Include(po => po.PurchaseOrderItem)
                    .ThenInclude(item => item.Unit)
                .Include(po => po.Warehouse)
                .Include(po => po.Currency)

                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrder
        public async Task<IActionResult> Index()
        {
            var purchaseOrders = await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.PurchaseOrderItem)
                .ToListAsync();

            return View(purchaseOrders);
        }

        // GET: PurchaseOrder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(po => po.Supplier)
                .Include(po => po.PurchaseOrderItem)
                    .ThenInclude(item => item.Item)
                .Include(po => po.PurchaseOrderItem)
                    .ThenInclude(item => item.Unit)
                .FirstOrDefaultAsync(m => m.PurchaseOrderId == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchaseOrder = await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderItem)
                .FirstOrDefaultAsync(po => po.PurchaseOrderId == id);

            if (purchaseOrder != null)
            {
                // First delete the related items
                _context.PurchaseOrderItems.RemoveRange(purchaseOrder.PurchaseOrderItem);

                // Then delete the order
                _context.PurchaseOrders.Remove(purchaseOrder);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: PurchaseOrder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrders
                .Include(po => po.PurchaseOrderItem)
                .FirstOrDefaultAsync(po => po.PurchaseOrderId == id);

            if (purchaseOrder == null)
            {
                return NotFound();
            }

            //purchaseOrder.PurchaseOrderItem ??= new List<PurchaseOrderItem>();

            ViewBag.Suppliers = await _context.Suppliers.ToListAsync();
            ViewBag.Items = await _context.Items.Include(i => i.Unit).ToListAsync();
            ViewBag.Units = await _context.Units.ToListAsync();
            ViewBag.Warehouses = await _context.Warehouses.ToListAsync();
            ViewBag.Currencies = await _context.Currencies.ToListAsync();


            return View(purchaseOrder);
        }

        // POST: PurchaseOrder/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PurchaseOrder purchaseOrder)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Load existing PurchaseOrder with its items
                    var existingPO = await _context.PurchaseOrders
                        .Include(p => p.PurchaseOrderItem)
                        .FirstOrDefaultAsync(p => p.PurchaseOrderId == purchaseOrder.PurchaseOrderId);

                    if (existingPO == null)
                    {
                        return NotFound();
                    }

                    // Update PurchaseOrder fields
                    existingPO.PONo = purchaseOrder.PONo;
                    existingPO.SupplierId = purchaseOrder.SupplierId;
                    existingPO.OrderDate = purchaseOrder.OrderDate;
                    existingPO.Warehouse = purchaseOrder.Warehouse;
                    existingPO.DeliveryPoint = purchaseOrder.DeliveryPoint;
                    existingPO.WarehouseId = purchaseOrder.WarehouseId;
                    existingPO.CurrencyId = purchaseOrder.CurrencyId;

                    // Remove existing items
                    _context.PurchaseOrderItems.RemoveRange(existingPO.PurchaseOrderItem);

                    // Add updated/new items
                    foreach (var item in purchaseOrder.PurchaseOrderItem)
                    {
                        item.PurchaseOrderItemId = 0; // EF will auto generate new Id
                        existingPO.PurchaseOrderItem.Add(item);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating purchase order: " + ex.Message);
                }
            }

            // Reload ViewBag data
            ViewBag.Suppliers = _context.Suppliers.ToList();
            ViewBag.Items = _context.Items.Include(i => i.Unit).ToList();
            ViewBag.Units = _context.Units.ToList();
            ViewBag.Warehouses = _context.Warehouses.ToList();
            ViewBag.Currencies = _context.Currencies.ToList();


            return View(purchaseOrder);
        }


        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrders.Any(e => e.PurchaseOrderId == id);
        }


    }
}
