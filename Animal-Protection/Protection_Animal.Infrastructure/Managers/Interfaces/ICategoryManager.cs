using Protection_Animal.Model.Entities;


namespace Protection_Animal.Infrastructure.Managers.Interfaces
{
    public interface ICategoryManager
    {
        public List<ApplicationCategory> GetAll();
        public ApplicationCategory Create(ApplicationCategory applicationCategory);
        public ApplicationCategory Update(ApplicationCategory applicationCategory, int id);
        public ApplicationCategory Delete(int id);
        public ApplicationCategory GetById(int id);
        public bool IsExists(int id);
    }
}
