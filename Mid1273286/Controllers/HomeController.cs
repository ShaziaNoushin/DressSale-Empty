using Microsoft.AspNetCore.Mvc;

namespace Mid1273286.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
