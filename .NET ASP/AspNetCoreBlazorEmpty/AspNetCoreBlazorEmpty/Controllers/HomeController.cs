using AspNetCoreBlazorEmpty.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreBlazorEmpty.Controllers
{
    public class HomeController : Controller
    {

        private BlazorTDBContext _context;
        public HomeController(BlazorTDBContext context)
        {
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View(View)
        // //   return View(viewName:"sample",);
        //}
//        public IActionResult Index(FromQuery] string selectedManufacturer)
        public IActionResult Index([FromQuery] string selectedManufacturer)
        {
            return View(new ProductListViewModel
            {
                Products= _context.Products.Include(p=>p.ProductCategory).Include(p=>p.ProductManufacturer),
                // 모든 products에 대해서 외래키 참조관계를 통해   ProductCategory ,ProductManufacturer 를 조인해서 가져옴
                Manufacturer = _context.Manufacturers.Select(m=>m.ManufacturerName).Distinct(),
                // 모든 제조사에 대해중복 없이 
                SelectedManufacturer =  selectedManufacturer    
                // 인자로 들어온 현재 제조사값을 할당*
            }); 
        }
        // views에 Home에 index.cshtml에 찾아 랜더링 
    }
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set;} = Enumerable.Empty<Product>();        
        public IEnumerable<string> Manufacturer { get; set; } = Enumerable.Empty<string>(); //제조사 이름
        public string SelectedManufacturer { get; set; } = string.Empty; // 드롭다운에서 선택된  제조사 이름값
        public string GetClass(string? Manufacturer) => SelectedManufacturer == Manufacturer ? "bg-info text-white" : "";

    }
}
