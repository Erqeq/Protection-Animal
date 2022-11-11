using Animal_Protection.Data;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Model.Entities;

namespace Protection_Animal.Controller
{
    public class ApllicationCategoryController
    {
        private IRepository<ApplicationCategory> _repository;
        public ApllicationCategoryController(IRepository<ApplicationCategory> repository)
        {
            AppDbContext context = new AppDbContext();
            _repository = repository;
        }
        public ApplicationCategory Create(ApplicationCategory applicationCategory)
        {
            return _repository.Create(applicationCategory);
        }

    }
}
