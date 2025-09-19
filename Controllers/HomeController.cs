using System.Diagnostics;
using _23WebC_Nhom10.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PagedList;


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
        /*
        * Tên: Bùi Huy Khang
        * Date:19-09-2025
        * Action Index hiển thị danh sách người dùng có phân trang
        */
        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var users = _userStore.Users.ToList();
            var pagedUsers = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.TotalItems = users.Count;
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;

            return View(pagedUsers);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*
        * Tên: Bùi Huy Khang
        * Date:18-09-2025
        * Action NotFound
        */
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
