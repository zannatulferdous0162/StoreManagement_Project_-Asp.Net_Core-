using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;

namespace StoreManagement_Project.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _context.Items
                .Include(i => i.Category)        // Include the related Category
                .Include(i => i.SubCategory)     // Include the related SubCategory
                .Include(i => i.Brand)           // Include the related Brand
                .Include(i => i.Unit)             // Include the related UOM
                .Include(i => i.PackSize)        // Include the related PackSize
                .ToListAsync();

            return View(items);  // Pass the list of items to the view
        }


        [HttpGet]
        public IActionResult Create()
        {
            var model = new ItemViewModel();
            PopulateDropdowns(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the latest item to determine the next ItemCode
                var lastItem = await _context.Items
                    .OrderByDescending(i => i.ItemId)
                    .FirstOrDefaultAsync();

                int nextCode = 101; // Default starting code
                if (lastItem != null &&
                    !string.IsNullOrEmpty(lastItem.ItemCode) &&
                    lastItem.ItemCode.StartsWith("Item-") &&
                    int.TryParse(lastItem.ItemCode.Replace("Item-", ""), out int lastCode))
                {
                    nextCode = lastCode + 1;
                }

                var item = new Item
                {
                    ItemCode = $"Item-{nextCode}", // Auto-generated ItemCode like "Item-101"
                    ItemName = model.ItemName,
                    CategoryId = model.CategoryId,
                    SubCategoryId = model.SubCategoryId,
                    BrandId = model.BrandId,
                    UnitId = model.UnitId,
                    PackSizeId = model.PackSizeId
                };

                _context.Items.Add(item);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index"); // Redirect to list page
            }

            PopulateDropdowns(model); // Refill dropdowns on error
            return View(model);
        }
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.SubCategory)
                .Include(i => i.Brand)
                .Include(i => i.Unit)
                .Include(i => i.PackSize)
                .FirstOrDefaultAsync(i => i.ItemId == id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        
           
            // GET: Item/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var item = await _context.Items.FindAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                var viewModel = new ItemEditViewModel
                {
                    Id = item.ItemId,
                    Name = item.ItemName,
                    CategoryId = item.CategoryId,
                    SubCategoryId = item.SubCategoryId,
                    BrandId = item.BrandId,
                    UnitId = item.UnitId,
                    PackSizeId = item.PackSizeId
                };

                await PopulateDropdowns(viewModel);
                return View(viewModel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, ItemEditViewModel viewModel)
            {
                if (id != viewModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var item = await _context.Items.FindAsync(id);
                        if (item == null)
                        {
                            return NotFound();
                        }

                        // Update the item with viewModel values
                        item.ItemName = viewModel.Name;
                        item.CategoryId = viewModel.CategoryId;
                        item.SubCategoryId = viewModel.SubCategoryId;
                        item.BrandId = viewModel.BrandId;
                        item.UnitId = viewModel.UnitId;
                        item.PackSizeId = viewModel.PackSizeId;

                        _context.Update(item);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemExists(viewModel.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                await PopulateDropdowns(viewModel);
                return View(viewModel);
            }

            private bool ItemExists(int id)
            {
                return _context.Items.Any(e => e.ItemId == id);
            }

            private async Task PopulateDropdowns(ItemEditViewModel viewModel)
            {
                viewModel.CategoryOptions = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", viewModel.CategoryId);
                viewModel.SubCategoryOptions = new SelectList(await _context.SubCategories.ToListAsync(), "Id", "Name", viewModel.SubCategoryId);
                viewModel.BrandOptions = new SelectList(await _context.Brands.ToListAsync(), "Id", "Name", viewModel.BrandId);
                viewModel.UnitOptions = new SelectList(await _context.Units.ToListAsync(), "Id", "Name", viewModel.UnitId);
                viewModel.PackSizeOptions = new SelectList(await _context.PackSizes.ToListAsync(), "Id", "Name", viewModel.PackSizeId);
            }

        // GET: Item/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item); // Remove the item from the database
            await _context.SaveChangesAsync(); // Save changes to the database

            return RedirectToAction(nameof(Index)); // Redirect back to the index view
        }


        [HttpGet]
        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            var subCategories = await _context.SubCategories
                .Where(sc => sc.CategoryId == categoryId)
                .Select(sc => new SelectListItem
                {
                    Value = sc.SubCategoryId.ToString(),
                    Text = sc.SubCategoryName
                })
                .ToListAsync();

            return Json(subCategories);
        }

        private void PopulateDropdowns(ItemViewModel model)
        {
            model.Categories = _context.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

            model.Brands = _context.Brands
                .Select(b => new SelectListItem
                {
                    Value = b.BrandId.ToString(),
                    Text = b.BrandName
                }).ToList();

            model.Units = _context.Units
                .Select(u => new SelectListItem
                {
                    Value = u.UnitId.ToString(),
                    Text = u.NameOfUnit
                }).ToList();

            model.PackSizes = _context.PackSizes
                .Select(p => new SelectListItem
                {
                    Value = p.PackSizeId.ToString(),
                    Text = p.Size
                }).ToList();

            // Only load subcategories if a category is already selected
            if (model.CategoryId > 0)
            {
                model.SubCategories = _context.SubCategories
                    .Where(sc => sc.CategoryId == model.CategoryId)
                    .Select(sc => new SelectListItem
                    {
                        Value = sc.SubCategoryId.ToString(),
                        Text = sc.SubCategoryName
                    }).ToList();
            }
        }
    }
}