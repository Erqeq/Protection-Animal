using Animal_Protection.Data;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Model.Entities;

namespace StudentManager.Model.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : BaseEntity
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
        public T ReadById(int id)
        {
            var entity = Ctx.Set<T>().FirstOrDefault(en => en.Id == id);
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
        public void DeleteById(int id)
        {
            var entity = ReadById(id);
            Ctx.Set<T>().Remove(entity);

            Ctx.SaveChanges();
        }
    }
}
