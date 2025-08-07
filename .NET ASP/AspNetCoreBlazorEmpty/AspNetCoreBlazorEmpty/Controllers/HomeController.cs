using AspNetCoreBlazorEmpty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
using System;
using System.Diagnostics;

namespace AspNetCoreBlazorEmpty.Controllers
{
    // {}Controller 로 작명시  : Controller를 상속받은 클래스는 
    public class HomeController : Controller
    {
        private BlazorTDBContext context;
        public HomeController(BlazorTDBContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index([FromQuery] string selectedManufacturer)
        {
            return View(new ProductListViewModel
            {
                // 초기진입시 모든 product
                Product = context.Products.Include(p => p.ProductCategory).Include(p => p.ProductManufacturer),
                Manufacturer = context.Manufacturers.Select(m => m.ManufacturerName).Distinct(),
                SelectedManufacturer = selectedManufacturer
            });
        }
    }

    public class ProductListViewModel
    {
        public IEnumerable<Product> Product { get; set; } = Enumerable.Empty<Product>();
        public IEnumerable<string> Manufacturer { get; set; } = Enumerable.Empty<string>();
        public string SelectedManufacturer { get; set; } = String.Empty;
        public string GetClass(string Manufacturer) {
            Debug.Print($"Selected 값:{SelectedManufacturer},들어온 Manufacture 값{Manufacturer} ");
            Debug.Print(SelectedManufacturer == Manufacturer ? "bg-info text-white" : "");
            return SelectedManufacturer == Manufacturer ? "bg-info text-white" : "";

        }
    }
}