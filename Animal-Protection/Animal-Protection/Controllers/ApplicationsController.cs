using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Protection.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Protection_Animal.Utility;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Application = Protection_Animal.Model.Entities.Application;
using NLog;
using NLog.Fluent;

namespace Animal_Protection.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IApplicationManager _applicationManager;
        private readonly ICategoryManager _categoryManager;
        private readonly IAnimalManager _animalManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ApplicationsController(AppDbContext context, 
            IWebHostEnvironment webHostEnvironment, 
            IApplicationManager applicationManager,
            ICategoryManager categoryManager, 
            IAnimalManager animalManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _applicationManager = applicationManager;
            _categoryManager = categoryManager;
            _animalManager = animalManager;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            logger.Info("the user received all applications");
            var applications = _applicationManager.GetAll();

            if (applications == null)
            {
                logger.Info("no applications");
                return NotFound();
            }

            return View(applications);
        }

        //GET: Applications/Details/5
        public async Task<IActionResult> Details(int id)
        {
            logger.Info("the user has received details of application");
            var application = _applicationManager.GetById(id);
            if (application == null)
            {
                logger.Info("no details");
                return NotFound();
            }
            return View(application);
        }
        //POST
        public async Task<IActionResult> DetailsChange(int id)
        {
            var falsevalue = _applicationManager.GetById(id);
            if (falsevalue.IsActive == false)
            {
                logger.Info("the user update Is active to true");
                falsevalue.IsActive = true;
            }
            else
            {
                logger.Info("the user update Is active to false");
                falsevalue.IsActive = false;
            }
            _applicationManager.Update(falsevalue, id);
            return RedirectToAction(nameof(Index));
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_categoryManager.GetAll(), "Id", "Name");
            ViewData["AnimalId"] = new SelectList(_animalManager.GetAll(), "Id", "Name");

            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShortDesciption, Description,CategoryId,AnimalId,Id,Name,ImageFile")] Application application)
        {
            logger.Info("the user created an applications with photo");
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            string upload = webRootPath + WebConstants.ImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            application.Image = fileName + extension;

            application.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _applicationManager.Create(application);

            return RedirectToAction("index", "Home");
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            logger.Info($"edited an application");
            var application = _applicationManager.GetById(id);
            if (application == null)
            {
                return NotFound();
            }

            ViewData["AnimalId"] = new SelectList(_animalManager.GetAll(), "Id", "Name", application.AnimalId);
            ViewData["CategoryId"] = new SelectList(_categoryManager.GetAll(), "Id", "Name", application.CategoryId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ShortDesciption, Description,CategoryId,IsActive,AnimalId,SenderId,Id,Name,ImageFile")] Application application, int id)
        {
            var objectFromDb = _applicationManager.GetById(id);

            if (!application.Id.Equals(id))
            {
                return NotFound();
            }

            try
            {
                logger.Info("the user edited an application");
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (files.Count > 0)
                {
                    string upload = webRootPath + WebConstants.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(upload, objectFromDb.Image);

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    application.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    application.Image = fileName + extension;
                }
                else
                {
                    logger.Info("the user edited an application without changing an image");
                    application.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    application.Image = objectFromDb.Image;
                }
                _applicationManager.Update(application, id);

                return RedirectToAction("index", "Home");

            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Error(ex);
            }
            return RedirectToAction("index", "Home");
        }

    }
}
