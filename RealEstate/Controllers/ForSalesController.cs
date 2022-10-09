using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class ForSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ForSales
        public async Task<IActionResult> Index()
        {
              return View(await _context.ForSale.ToListAsync());
        }

        // GET: ForSales/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForSale == null)
            {
                return NotFound();
            }

            var forSale = await _context.ForSale
                .FirstOrDefaultAsync(m => m.ForSaleId == id);
            if (forSale == null)
            {
                return NotFound();
            }

            return View(forSale);
        }

        // GET: ForSales/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForSaleId,Name,Price,Description,Photo,Rooms,SqFootage,CityId")] ForSale forSale)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forSale);
        }

        // GET: ForSales/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForSale == null)
            {
                return NotFound();
            }

            var forSale = await _context.ForSale.FindAsync(id);
            if (forSale == null)
            {
                return NotFound();
            }
            return View(forSale);
        }

        // POST: ForSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForSaleId,Name,Price,Description,Photo,Rooms,SqFootage,CityId")] ForSale forSale)
        {
            if (id != forSale.ForSaleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forSale);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForSaleExists(forSale.ForSaleId))
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
            return View(forSale);
        }

        // GET: ForSales/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForSale == null)
            {
                return NotFound();
            }

            var forSale = await _context.ForSale
                .FirstOrDefaultAsync(m => m.ForSaleId == id);
            if (forSale == null)
            {
                return NotFound();
            }

            return View(forSale);
        }

        // POST: ForSales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForSale == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ForSale'  is null.");
            }
            var forSale = await _context.ForSale.FindAsync(id);
            if (forSale != null)
            {
                _context.ForSale.Remove(forSale);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForSaleExists(int id)
        {
          return _context.ForSale.Any(e => e.ForSaleId == id);
        }
    }
}
