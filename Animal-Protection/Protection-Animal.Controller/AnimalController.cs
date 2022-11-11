using Animal_Protection.Data;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Model.Entities;
using StudentManager.Model.Repositories;

namespace Protection_Animal.Controller
{
    public class AnimalController
    {
        private IRepository<Animal> _animalRepository;
        public AnimalController(IRepository<Animal> repository)
        {
            AppDbContext context = new AppDbContext();
            _animalRepository = new DbRepository<Animal>(context);
        }
        public Animal Create(Animal animal)
        {
            return _animalRepository.Create(animal);
        }

    }
}
