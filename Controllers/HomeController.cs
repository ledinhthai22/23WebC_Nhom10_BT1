using System.Diagnostics;
using _23WebC_Nhom10.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _23WebC_Nhom10.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserStore _userStore;
    
        public HomeController(ILogger<HomeController> logger, UserStore userStore)
        {
            _logger = logger;
            _userStore = userStore;
        }

        public IActionResult Index()
        {
            return View(_userStore.Users);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult NotFound()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
