using Protection_Animal.Model.Entities;

namespace ProjectAnimal.Model.Repository
{
    public interface IRepository<T, TId> where T : BaseEntity<T, TId>
    {
        public T Create(T entity);
        public T Update(T entity);
        public IQueryable<T> GetAll();
        public T GetById(TId id);
        public T Delete(TId id);
        public T DeleteById(TId id);
        public bool IsExists(TId id);
    }
}