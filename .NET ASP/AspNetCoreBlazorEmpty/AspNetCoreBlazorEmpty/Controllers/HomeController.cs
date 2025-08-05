using AspNetCoreBlazorEmpty.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreBlazorEmpty.Controllers
{
    public class HomeController : Controller
    {

        private BlazorTDBContext? _context;
        public HomeController(BlazorTDBContext? context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
//        public IActionResult Index(FromQuery] string selectedManufacturer)

    }
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set;} = Enumerable.Empty<Product>();        
        public IEnumerable<string> Manufacturer { get; set; } = Enumerable.Empty<string>(); //제조사 이름
        public string SelectedManufacturer { get; set; } = string.Empty; // 드롭다운에서 선택된  제조사 이름값
        public string GetClass(string? Manufacturer) => SelectedManufacturer == Manufacturer ? "bg-info text-white" : "";

    }
}
