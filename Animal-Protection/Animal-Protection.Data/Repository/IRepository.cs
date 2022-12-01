using Protection_Animal.Model.Entities;

namespace ProjectAnimal.Model.Repository
{
    public interface IRepository<T,TId> where T : BaseEntity<T, TId>
    {
        public T Create(T entity);
        public T Update(T entity);
        public List<T> ReadAll();
        public T ReadById(TId id);
        public void Delete(T entity);
        public void DeleteById(TId id);

    }
}