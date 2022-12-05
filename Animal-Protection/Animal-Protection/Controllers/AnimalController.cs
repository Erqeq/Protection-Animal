using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Protection.Data;
using Protection_Animal.Model.Entities;
using Protection_Animal.Utility;
using System.Drawing;
using Protection_Animal.Infrastructure.Managers.Interfaces;

namespace Animal_Protection.Controllers
{
    public class AnimalController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IAnimalManager _animalManager;
        
        public AnimalController(AppDbContext context, IWebHostEnvironment webHostEnvironment, IAnimalManager animalManager)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _animalManager = animalManager;
            
        }

        // GET: Animal
        public async Task<IActionResult> Index()
        {
            var allAnimals = _animalManager.GetAll();
            return View(allAnimals);
        }

        // GET: Animal/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            //var animal = await _context.Animals
            //    .FirstOrDefaultAsync(m => m.Id.Equals(id));

            var animal = _animalManager.Details(id);
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
            if (!ModelState.IsValid)
            {
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

                //_context.Add(animal);
                //await _context.SaveChangesAsync();

                _animalManager.Create(animal);
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animal/Edit/5
        public async Task<IActionResult> Edit(int/*?*/ id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            
            var animal = _animalManager.ReadById(id);
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
            var objectFromDb = /*_context.Animals.AsNoTracking().FirstOrDefault(u => u.Id == animal.Id);*/ _animalManager.ObjectfromDb(id);

            if (!animal.Id.Equals(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                try
                {

                    var files = HttpContext.Request.Form.Files;
                    string webRootPath = _webHostEnvironment.WebRootPath;
                    if(files.Count > 0)
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
                    
                    var updateanimal = _animalManager.Update(animal);

                    //_context.Update(animal);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                
                }
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        // GET: Animal/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            //var animal = await _context.Animals
            //    .FirstOrDefaultAsync(m => m.Id.Equals(id));

            var animal = _animalManager.ReadById(id);

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
            if (_context.Animals == null)
            {
                return Problem("Entity set 'AppDbContext.Animals'  is null.");
            }

            var animal = /*await _context.Animals.FindAsync(id);*/ _animalManager.ReadById(id);

            string upload = _webHostEnvironment.WebRootPath + WebConstants.ImagePath;

            var imagePath = Path.Combine(upload, animal.Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (animal != null)
            {
                _animalManager.Delete(id);
            }
            
            
            return RedirectToAction(nameof(Index));
        }

    }
}
