using Protection_Animal.Model.Entities;

namespace ProjectAnimal.Model.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        public T Create(T entity);
        public T Update(T entity);
        public List<T> ReadAll();
        public T ReadById(int id);
        public void Delete(T entity);
        public void DeleteById(int id);

    }
}