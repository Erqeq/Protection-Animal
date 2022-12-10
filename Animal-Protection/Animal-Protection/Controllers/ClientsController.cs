using Microsoft.AspNetCore.Mvc;
using Animal_Protection.Data;
using Protection_Animal.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using Animal_Protection.Areas.Identity.Data;
using Protection_Animal.Utility;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Animal_Protection.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IClientManager _clientManager;
        private readonly UserManager<AnimalProtectionUser> _userManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
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
            logger.Info("admin got all users");
            var getAllClients = _clientManager.GetAll();
            return View(getAllClients);
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            logger.Info("got details of users");
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
            logger.Info("got list of users to delete");
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
            logger.Info("deleted user");
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
            logger.Info("users were duplicated from one database to another");
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