using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstate.Data;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ForSalesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForSalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ForSales
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ForSale.Include(p => p.City).OrderBy(p => p.Name);
            return View("Index", await applicationDbContext.ToListAsync());
        }

        // GET: ForSales/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForSale == null)
            {
                return NotFound();
            }

            var forSale = await _context.ForSale
                .Include(p => p.City)
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
            ViewData["CityId"] = new SelectList(_context.Cities.OrderBy(c => c.Name), "CityId", "Name");
            return View();
        }

        // POST: ForSales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForSaleId,Name,Price,Description,Photo,Rooms,SqFootage,CityId")] ForSale forSale, IFormFile? Photo)
        {
            ModelState.Remove("City");
            if (ModelState.IsValid)
            {
                if (Photo != null)
                {
                    var fileName = UploadPhoto(Photo);
                    forSale.Photo = fileName;
                }

                _context.Add(forSale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", forSale.CityId);
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", forSale.CityId);
            return View(forSale);
        }

        // POST: ForSales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForSaleId,Name,Price,Description,Photo,Rooms,SqFootage,CityId")] ForSale forSale, string? CurrentPhoto)
        {
            if (id != forSale.ForSaleId)
            {
                return NotFound();
            }
            ModelState.Remove("City");
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
            ViewData["CityId"] = new SelectList(_context.Cities, "CityId", "Name", forSale.CityId);
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
                .Include(p => p.City)
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
        private static string UploadPhoto(IFormFile Photo)
        { 
            var fileName = Guid.NewGuid() + "-" + Photo.FileName; //encrypts picture

            var uploadPath = System.IO.Directory.GetCurrentDirectory() + "\\wwwroot\\NewFolder\\NewFolder\\" + fileName; //folder ** unable to change name for some reason
            using (var stream = new FileStream(uploadPath, FileMode.Create))
            {
                Photo.CopyTo(stream);
            }

            return fileName;
        }
    }
}
