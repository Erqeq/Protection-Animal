using Protection_Animal.Model.Entities;

namespace Protection_Animal.Infrastructure.Managers.Interfaces
{
    public interface IApplicationManager
    {
        public List<Application> GetAll();

        public Application GetById(int id);
        public Application Create(Application entity);
        public Application Update(Application entity, int id);
        

    }
}
