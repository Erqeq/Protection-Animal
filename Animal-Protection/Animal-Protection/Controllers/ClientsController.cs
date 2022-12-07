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
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Infrastructure.Managers.Implemetations;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Animal_Protection.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientManager _clientManager;
        private readonly UserManager<AnimalProtectionUser> _userManager;
        public ClientsController(AppDbContext context,
            IClientManager clientManager, UserManager<AnimalProtectionUser> userManager)
        {
            _clientManager = clientManager;
            _userManager = userManager;
        }

        // GET: Clients
        [Authorize(Roles = WebConstants.Admin)]
        public async Task<IActionResult> Index()
        {
            var getAllClients = _clientManager.GetAll();
            return View(getAllClients);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            var client2 = _clientManager.GetById(id);
            if (client2 == null)
            {
                return NotFound();
            }
            return View(client2);
        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            var client = _clientManager.GetById(id);

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

            var client = _clientManager.GetById(id);
            if (client != null)
            {
                _clientManager.Delete(id);

                //var clientFromIdentity = await _identityContext.Users.FindAsync(id);
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
            }

            return RedirectToAction(nameof(Index));
        }

        public void AddUser(AnimalProtectionUser user)
        {
            var animalProtectionUser = MapUser(user);

            _clientManager.Create(animalProtectionUser);
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