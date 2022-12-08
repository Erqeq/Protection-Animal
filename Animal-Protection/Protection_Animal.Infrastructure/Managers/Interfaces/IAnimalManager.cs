using Protection_Animal.Model.Entities;

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
