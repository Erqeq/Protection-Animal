using Animal_Protection.Data;
using Animal_Protection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Protection_Animal.Model.Entities.ViewModels;
using System.Diagnostics;

namespace Animal_Protection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            logger.Info("The user went to the home page");
            HomeVM homeVM = new HomeVM()
            {
                Applications = _context.Applications.Include(u => u.Category).Include(u=>u.Sender).Where(x => x.IsActive == true),
                Categories = _context.Categories
            };
            
            return View(homeVM);
        }

        public IActionResult Privacy()
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