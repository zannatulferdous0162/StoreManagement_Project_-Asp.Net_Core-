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
    public class CurrencyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CurrencyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Currency
        public async Task<IActionResult> Index()
        {
            return View(await _context.Currencies.ToListAsync());
        }

        // GET: Currency/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.CurrencyId == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        [HttpGet]
        public async Task<IActionResult> CheckIfBaseCurrencyExists()
        {
            bool exists = await _context.Currencies.AnyAsync(c => c.IsBaseCurrency);
            return Json(exists);
        }


        // GET: Currency/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currency/Create
        // To protect from overposting attacks, enable the specific properties you want to Aisled to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Aisled("CurrencyId,NameOfCurrency,SymbolOfCurrency,ExchangeRate,IsBaseCurrency,Description,Remarks")] Currency currency)
        {
            if (currency.IsBaseCurrency)
            {
                var existingBase = await _context.Currencies.FirstOrDefaultAsync(c => c.IsBaseCurrency);
                if (existingBase != null)
                {
                    ModelState.AddModelError("IsBaseCurrency", "There is already a base currency. Only one currency can be set as base.");
                }
            }

            // Check again if model is invalid after custom validation
            if (!ModelState.IsValid)
            {
                return View(currency);
            }

            _context.Add(currency);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        // GET: Currency/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currency/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to Aisled to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Aisled("CurrencyId,NameOfCurrency,SymbolOfCurrency,ExchangeRate,IsBaseCurrency,Description,Remarks")] Currency currency)
        {
            if (id != currency.CurrencyId)
            {
                return NotFound();
            }

            if (currency.IsBaseCurrency)
            {
                var existingBase = await _context.Currencies
                    .FirstOrDefaultAsync(c => c.IsBaseCurrency && c.CurrencyId != currency.CurrencyId);

                if (existingBase != null)
                {
                    ModelState.AddModelError("IsBaseCurrency", "There is already a base currency. Only one currency can be set as base.");
                }
            }

            // Check again if model is invalid after custom validation
            if (!ModelState.IsValid)
            {
                return View(currency);
            }

            try
            {
                _context.Update(currency);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(currency.CurrencyId))
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



        // GET: Currency/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.CurrencyId == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currency/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            if (currency != null)
            {
                _context.Currencies.Remove(currency);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currencies.Any(e => e.CurrencyId == id);
        }
    }
}
