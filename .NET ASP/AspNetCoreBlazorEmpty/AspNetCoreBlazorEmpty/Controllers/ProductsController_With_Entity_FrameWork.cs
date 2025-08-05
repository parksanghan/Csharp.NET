using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCoreBlazorEmpty.Models;

namespace AspNetCoreBlazorEmpty.Controllers
{
    public class ProductsController_With_Entity_FrameWork : Controller
    {
        private readonly BlazorTDBContext _context;

        public ProductsController_With_Entity_FrameWork(BlazorTDBContext context)
        {
            _context = context;
        }

        // GET: ProductsController_With_Entity_FrameWork
        public async Task<IActionResult> Index()
        {
            var blazorTDBContext = _context.Products.Include(p => p.ProductCategory).Include(p => p.ProductManufacturer);
            return View(await blazorTDBContext.ToListAsync());
        }

        // GET: ProductsController_With_Entity_FrameWork/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductManufacturer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: ProductsController_With_Entity_FrameWork/Create
        public IActionResult Create()
        {
            ViewData["ProductCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["ProductManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerId");
            return View();
        }

        // POST: ProductsController_With_Entity_FrameWork/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,ProductPrice,ProductCategoryId,ProductManufacturerId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.ProductCategoryId);
            ViewData["ProductManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerId", product.ProductManufacturerId);
            return View(product);
        }

        // GET: ProductsController_With_Entity_FrameWork/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["ProductCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.ProductCategoryId);
            ViewData["ProductManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerId", product.ProductManufacturerId);
            return View(product);
        }

        // POST: ProductsController_With_Entity_FrameWork/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,ProductPrice,ProductCategoryId,ProductManufacturerId")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            ViewData["ProductCategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.ProductCategoryId);
            ViewData["ProductManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerId", product.ProductManufacturerId);
            return View(product);
        }

        // GET: ProductsController_With_Entity_FrameWork/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.ProductManufacturer)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: ProductsController_With_Entity_FrameWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
