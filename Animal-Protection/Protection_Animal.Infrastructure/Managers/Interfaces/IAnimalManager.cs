using Protection_Animal.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protection_Animal.Infrastructure.Managers.Interfaces
{
    public interface IAnimalManager
    {
        public List<Animal> GetAll();
        public Animal Details(int id);
        public Animal Create(Animal animal);
        public Animal GetById(int id);
        public Animal Update(Animal animal, int id);
        public Animal Delete(int id);
    }
}
