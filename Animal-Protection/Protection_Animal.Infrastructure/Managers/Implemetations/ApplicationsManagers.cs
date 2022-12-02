using ProjectAnimal.Model.Repository;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Model.Entities;

namespace Protection_Animal.Infrastructure.Managers.Implemetations
{
    internal class ApplicationsManagers : IApplicationManager
    {
        private readonly IRepository<Application, int> _repository;

        public ApplicationsManagers(IRepository<Application, int> repository)
        {
            _repository = repository;
        }
        public List<Application> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
