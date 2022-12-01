using Animal_Protection.Data;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Model.Entities;

namespace StudentManager.Model.Repositories
{
    public class DbRepository<T, TId> : IRepository<T, TId> where T : BaseEntity<T, TId>
    {
        protected readonly AppDbContext Ctx;

        public DbRepository(AppDbContext ctx)
        {
            Ctx = ctx;
        }
        public T Create(T entity)
        {
            var a = Ctx.Set<T>().Add(entity);
            Ctx.SaveChanges();

            return a.Entity;
        }
        public List<T> ReadAll()
        {
            var list = Ctx.Set<T>().ToList();

            return list;
        }
        public T ReadById(TId id)
        {
            var entity = Ctx.Set<T>().FirstOrDefault(en => en.Id.Equals(id));
            return entity;
        }
        public T Update(T entity)
        {
            var a = Ctx.Set<T>().Update(entity);
            Ctx.SaveChanges();

            return a.Entity;
        }
        public void Delete(T entity)
        {
            Ctx.Set<T>().Remove(entity);

            Ctx.SaveChanges();
        }
        public void DeleteById(TId id)
        {
            var entity = ReadById(id);
            Ctx.Set<T>().Remove(entity);

            Ctx.SaveChanges();
        }
    }
}
