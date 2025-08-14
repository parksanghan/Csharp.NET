using Microsoft.AspNetCore.Mvc;

namespace BlazorServerSignalR.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
