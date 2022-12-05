using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Animal_Protection.Data;
using Protection_Animal.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Animal_Protection.Areas.Identity.Data;
using Protection_Animal.Utility;

namespace Animal_Protection.Controllers
{

    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IdentityContext _identityContext;
        public ClientsController(AppDbContext context, IdentityContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        // GET: Clients
        [Authorize(Roles = WebConstants.Admin)]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
               .Include(m => m.Applications)
               .FirstOrDefaultAsync(m => m.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
 
        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _context.Clients == null)
            {
                return NotFound();
            }

            var client = await _context.Clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'AppDbContext.Clients'  is null.");
            }
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                var clientFromIdentity = await _identityContext.Users.FindAsync(id);
                _identityContext.Users.Remove(clientFromIdentity);

            }
            await _identityContext.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool ClientExists(string id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
        public void AddUser(AnimalProtectionUser user)
        {
            var animalProtectionUser = MapUser(user);
            _context.Clients.Add(animalProtectionUser);
            _context.SaveChanges();
        }
        private Client MapUser(AnimalProtectionUser user)
        {
            
            return new Client()
            {
                Id = user.Id,
                Name = user.FullName,
                Email = user.Email,
                TelephoneNumber = user.PhoneNumber
            };
            
        }
    }
}
