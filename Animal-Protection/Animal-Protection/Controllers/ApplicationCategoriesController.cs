using Microsoft.AspNetCore.Mvc;
using Protection_Animal.Model.Entities;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using NLog;
using NLog.LayoutRenderers;

namespace Animal_Protection.Controllers
{
    public class ApplicationCategoriesController : Controller
    {
        private readonly ICategoryManager _categoryManager;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public ApplicationCategoriesController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        // GET: ApplicationCategories
        public async Task<IActionResult> Index()
        {
            logger.Info("the user received a list of all categories");
            var allCategories = _categoryManager.GetAll();
            return View(allCategories);
        }

        // GET: ApplicationCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] ApplicationCategory applicationCategory)
        {
            if (ModelState.IsValid)
            {
                logger.Info("the user created an animal");
                var category = _categoryManager.Create(applicationCategory);

                return RedirectToAction(nameof(Index));
            }
            return View(applicationCategory);
        }

        // GET: ApplicationCategories/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            logger.Info("the user is given a list of animals to edit");
            var applicationCategory = _categoryManager.GetById(id);

            if (applicationCategory == null)
            {
                return NotFound();
            }
            return View(applicationCategory);
        }

        // POST: ApplicationCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] ApplicationCategory applicationCategory)
        {
            if (id != applicationCategory.Id)
            {
                logger.Info("no categories");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                logger.Info("the user updated an category");
                var updateCategory = _categoryManager.Update(applicationCategory, id);
                if (updateCategory != null)
                { return RedirectToAction(nameof(Index)); }
            }
            return View(applicationCategory);
        }

        // GET: ApplicationCategories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            logger.Info("the user got an category to delete");
            var deleteCategory = _categoryManager.GetById(id);
            if (deleteCategory == null)
            {
                logger.Info("no categories");
                return NotFound();
            }

            return View(deleteCategory);
        }

        // POST: ApplicationCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            logger.Info("the user deleted an animal");
            var applicationCategory = _categoryManager.Delete(id);

            if (applicationCategory == null)
            {
                logger.Info("no categories");
                return Problem("Category is null.");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
