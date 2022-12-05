using Animal_Protection.Data;
using Microsoft.EntityFrameworkCore;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Model.Entities;


namespace StudentManager.Model.Repositories
{
    public class DbRepository<T, TId> : IRepository<T, TId> where T : BaseEntity<T, TId>
    {
        protected readonly AppDbContext _ctx;


        public DbRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public T Create(T entity)
        {
            var a = _ctx.Set<T>().Add(entity);
            _ctx.SaveChanges();

            return a.Entity;
        }
        public IQueryable<T> ReadAll()
        {
            if (_ctx.Set<T>() == null)
            {
                return null;
            }

            var list = _ctx.Set<T>();

            return list;
        }
        public T ReadById(TId? id)
        {
            if (_ctx.Set<T>() == null)
            {
                return null;
            }

            var entity = _ctx.Set<T>().AsNoTracking().FirstOrDefault(en => en.Id.Equals(id));
            return entity;
        }
        public T Update(T entity)
        {
            var a = _ctx.Set<T>().Update(entity);
            _ctx.SaveChanges();

            return a.Entity;
        }
        public T Delete(TId id)
        {
            var deleteEntity = _ctx.Set<T>().Find(id);

            _ctx.SaveChanges();

            return deleteEntity;

            
        }
        public T DeleteById(TId id)
        {
            var entity = ReadById(id);

            _ctx.Set<T>().Remove((T)entity);

            _ctx.SaveChanges();

            return entity;
        }

        public bool IsExists(TId id)
        {
            return _ctx.Set<T>().Any(entity => entity.Id.Equals(id));
        }
    }
}
