using Microsoft.EntityFrameworkCore;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Model.Entities;

namespace Protection_Animal.Infrastructure.Managers.Implemetations
{
    public class AnimalManager : IAnimalManager
    {
        private readonly IRepository<Animal, int> _repository;

        public AnimalManager(IRepository<Animal, int> repository)
        {
            _repository = repository;
        }

        public List<Animal> GetAll()
        {
            var allAnimal = _repository.ReadAll();

            if (allAnimal == null)
                return null;

            return allAnimal.ToList();

        }
        public Animal Create(Animal animal)
        {

            var createAnimal = _repository.Create(animal);

            if (createAnimal == null)
                return null;

            return createAnimal;
        }
        public Animal Details(int id)
        {
            var details = _repository.ReadById(id);

            if (details == null)
                return null;

            return details;
        }
        public Animal Update(Animal animal, int id)
        {
            try
            {
                _repository.Update(animal);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.IsExists(animal.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return animal;
        }
        public Animal Delete(int id)
        {
            var deleteAnimal = _repository.DeleteById(id);

            if (deleteAnimal == null)
            { return null; }

            return deleteAnimal;

        }
        public Animal GetById(int id)
        {
            return _repository.ReadById(id);
        }
        public bool IsExists(int id)
        {
            return _repository.IsExists(id);
        }
    }
}
