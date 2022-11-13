using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Protection.Data;
using Protection_Animal.Model.Entities;

namespace Animal_Protection.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly AppDbContext _context;

        public ApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Applications.Include(a => a.Animal).Include(a => a.Category).Include(a => a.Receiver).Include(a => a.Sender);
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
                .Include(a => a.Receiver)
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Description");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address");
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,DateCreate,IsActive,CategoryId,SenderId,ReceiverId,AnimalId,Id,Name")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Description", application.AnimalId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", application.CategoryId);
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address", application.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address", application.SenderId);
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Description", application.AnimalId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", application.CategoryId);
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address", application.ReceiverId);
            ViewData["SenderId"] = new SelectList(_context.Clients, "Id", "Address", application.SenderId);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,DateCreate,IsActive,CategoryId,SenderId,ReceiverId,AnimalId,Id,Name")] Application application)
        {
            if (id != application.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Description", application.AnimalId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", application.CategoryId);
            ViewData["ReceiverId"] = new SelectList(_context.Clients, "Id", "Address", application.ReceiverId);
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
                .Include(a => a.Receiver)
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
