using Microsoft.EntityFrameworkCore;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Animal Details(int id)
        {
            var details = _repository.ReadById(id);

            if (details == null)
                return null;

            return details;
        }
        public Animal Create(Animal animal)
        {

            var createAnimal = _repository.Create(animal);

            if (createAnimal == null)
                return null;

            return createAnimal;
        }
        public Animal ReadById(int id)
        {
            var updateByIdAnimal = _repository.ReadById(id);

            if (updateByIdAnimal == null)
                return null;

            return updateByIdAnimal;
        }
        public Animal ObjectfromDb(int id)
        {
            var objFromDb = _repository.ReadById(id);

            if (objFromDb == null)
                return null;

            return objFromDb;
        }

        public Animal Update(Animal animal)
        {
           
            var updateAnimal = _repository.Update(animal);

            if (updateAnimal == null)
                return null;

            return updateAnimal;
        }

        public Animal Delete(int id)
        {
            var deleteAnimal = _repository.DeleteById(id);

            if (deleteAnimal == null)
                return null;

            return deleteAnimal;

        }
    }
}
