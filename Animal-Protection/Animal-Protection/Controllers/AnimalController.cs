using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Protection_Animal.Model.Entities;
using Protection_Animal.Utility;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using NLog;

namespace Animal_Protection.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAnimalManager _animalManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public AnimalController(IWebHostEnvironment webHostEnvironment, IAnimalManager animalManager)
        {
            _webHostEnvironment = webHostEnvironment;
            _animalManager = animalManager;
        }

        // GET: Animal
        public async Task<IActionResult> Index()
        {
            logger.Info("The user looked at all the animals");
            var allAnimals = _animalManager.GetAll();
            return View(allAnimals);
        }

        // GET: Animal/Details/5
        public async Task<IActionResult> Details(int id)
        {
            logger.Info("The user brought out the details of all the animals");
            var animal = _animalManager.GetById(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // GET: Animal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Animal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateOfBirth,Description,ImageFile,Id,Name")] Animal animal)
        {
            logger.Info("The user went to the tab to create animals");
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;

            string upload = webRootPath + WebConstants.ImagePath;
            string fileName = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(files[0].FileName);

            using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
            {
                files[0].CopyTo(fileStream);
            }

            animal.Image = fileName + extension;

            _animalManager.Create(animal);
            return RedirectToAction(nameof(Index));
        }

        // GET: Animal/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            logger.Info("The user went to the tab to edit animals");
            var animal = _animalManager.GetById(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: Animal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Animal animal)
        {
            logger.Info("user wants to edit an animal");
            var objectFromDb = _animalManager.GetById(id);

            if (!animal.Id.Equals(id))
            {
                logger.Info("there are no animals");
                return NotFound();
            }
            try
            {
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

                    animal.Image = fileName + extension;
                }
                else
                {
                    animal.Image = objectFromDb.Image;
                }
                var updateanimal = _animalManager.Update(animal, id);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                logger.Error(ex);
                throw;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Animal/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            logger.Info("the user was given a list of animals to delete");
            var animal = _animalManager.GetById(id);

            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        // POST: Animal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            logger.Info("the user wants to remove the animal");
            var animal = _animalManager.Delete(id);

            string upload = _webHostEnvironment.WebRootPath + WebConstants.ImagePath;

            var imagePath = Path.Combine(upload, animal.Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (animal == null)
            {
                return Problem("Entity is null");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
