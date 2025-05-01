using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreManagement_Project.Data;
using StoreManagement_Project.Models;
using StoreManagement_Project.ViewModels;

namespace StoreManagement_Project.Controllers
{
    public class UnitSetWithUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnitSetWithUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var unitSets = await _context.UnitSets
                .Include(us => us.Units)
                .ToListAsync();

            return View(unitSets);
        }
        public IActionResult Create()
        {
            var vm = new UnitSetWithUnitsViewModel();
            vm.Units.Add(new UnitViewModel());
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UnitSetWithUnitsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Ensure only one base unit is selected
                if (viewModel.Units.Count(u => u.IsBaseUnit) > 1)
                {
                    ModelState.AddModelError("", "Only one unit can be marked as the base unit.");
                    return View(viewModel);
                }

                var unitSet = new UnitSet
                {
                    NameOfUnitSet = viewModel.NameOfUnitSet,
                    Description = viewModel.Description,
                    Remarks = viewModel.Remarks,
                    Units = viewModel.Units.Select(u => new Unit
                    {
                        NameOfUnit = u.NameOfUnit,
                        UnitFactor = u.UnitFactor,
                        IsBaseUnit = u.IsBaseUnit,
                        Description = u.Description,
                        Remarks = u.Remarks
                    }).ToList()
                };

                _context.UnitSets.Add(unitSet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

    }
}
