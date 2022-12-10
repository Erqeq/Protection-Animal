using Animal_Protection.Data;
using Animal_Protection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Model.Entities.ViewModels;
using System.Diagnostics;

namespace Animal_Protection.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApplicationManager _applicationManager;
        private readonly ICategoryManager _categoryManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public HomeController(IApplicationManager applicationManager, ICategoryManager categoryManager)
        {
            _applicationManager = applicationManager;
            _categoryManager = categoryManager;
            
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            logger.Info("The user went to the home page");
            HomeVM homeVM = new HomeVM()
            {
                Applications = _applicationManager.GetAll().Where(x => x.IsActive == true),
                Categories = _categoryManager.GetAll()
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