using AspNetCoreBlazorEmpty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
 
using System;

namespace AspNetCoreBlazorEmpty.Pages
{
    public class IndexModel : PageModel
    {
        private BlazorTDBContext context;

        public IndexModel(BlazorTDBContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Product> Product { get; set; } = Enumerable.Empty<Product>();
        public IEnumerable<string> Manufacturer { get; set; } = Enumerable.Empty<string>();
        [FromQuery]
        public string SelectedManufacturer { get; set; } = String.Empty;
            
        public void OnGet()
        {
            Product = context.Products.Include(p => p.ProductCategory).Include(p => p.ProductManufacturer);
            Manufacturer = context.Manufacturers.Select(m => m.ManufacturerName).Distinct();
        }

        public string GetClass(string? Manufacturer) => SelectedManufacturer == Manufacturer ? "bg-info text-white" : "";
    }
}