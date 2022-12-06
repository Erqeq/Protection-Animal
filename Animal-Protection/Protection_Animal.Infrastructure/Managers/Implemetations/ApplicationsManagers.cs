using Animal_Protection.Data;
using Microsoft.EntityFrameworkCore;
using ProjectAnimal.Model.Repository;
using Protection_Animal.Infrastructure.Managers.Interfaces;
using Protection_Animal.Model.Entities;

namespace Protection_Animal.Infrastructure.Managers.Implemetations
{
    public class ApplicationsManagers : IApplicationManager
    {
        private readonly IRepository<Application, int> _repository;


        public ApplicationsManagers(IRepository<Application, int> repository)
        {
            _repository = repository;

        }

        public List<Application> GetAll()
        {
            var allAppliations = _repository.ReadAll();

            if (allAppliations == null)
                return null;

            return allAppliations
                .Include(a => a.Animal)
                .Include(a => a.Category)
                .Include(a => a.Sender)
                .ToList();
        }

        public Application GetById(int id)
        {
            var applicationDetails = _repository.ReadAll()
                .Include(a => a.Animal)
                .Include(a => a.Category)
                .Include(a => a.Sender)
                .AsNoTracking()
                .FirstOrDefault(m => m.Id.Equals(id));

            if (applicationDetails == null)
                return null;

            return applicationDetails;
        }
        public Application Create(Application application)
        {
            var createApplication = _repository.Create(application);

            if (createApplication == null)
                return null;
            return createApplication;
        }
        public Application Update(Application application, int id)
        {
            try
            {
                _repository.Update(application);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_repository.IsExists(application.Id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
            return application;
        }

    }
}
