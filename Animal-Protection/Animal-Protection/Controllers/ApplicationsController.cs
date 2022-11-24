using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Protection.Data;
using Protection_Animal.Model.Entities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Hosting;
using Protection_Animal.Utility;

namespace Animal_Protection.Controllers
{
    [Authorize]
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ApplicationsController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Applications.Include(a => a.Animal).Include(a => a.Category).Include(a => a.Sender);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Animal)
                .Include(a => a.Category)
                .Include(a => a.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address");
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Name");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,CategoryId,AnimalId,Id,Name,ImageFile")] Application application)
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

                application.Image = fileName + extension;

                application.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name", application.AnimalId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", application.CategoryId);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name", application.AnimalId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", application.CategoryId);
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address", application.SenderId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,CategoryId,IsActive,AnimalId,SenderId,Id,Name,ImageFile")] Application application)
        {
            var objectFromDb = _context.Applications.AsNoTracking().FirstOrDefault(u => u.Id == application.Id);

            if (id != application.Id)
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
                        application.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        application.Image = fileName + extension;
                    }
                    else
                    {
                        application.SenderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        application.Image = objectFromDb.Image;
                    }
                   
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Name", application.AnimalId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", application.CategoryId);
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address", application.SenderId);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Applications == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.Animal)
                .Include(a => a.Category)
                .Include(a => a.Sender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Applications == null)
            {
                return Problem("Entity set 'AppDbContext.Applications'  is null.");
            }
            var application = await _context.Applications.FindAsync(id);

            string upload = _webHostEnvironment.WebRootPath + WebConstants.ImagePath;

            var imagePath = Path.Combine(upload, application.Image);

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (application != null)
            {
                _context.Applications.Remove(application);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.Id == id);
        }
    }
}
