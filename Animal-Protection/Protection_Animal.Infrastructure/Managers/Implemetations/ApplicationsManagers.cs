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

        //public List<Application> Details(int id)
        //{
        //    var applicationDetails = _repository.ReadById(id);

        //    if (applicationDetails == null)
        //        return null;

        //    //return applicationDetails
        //    //    .Include(a => a.Animal)
        //    //    .Include(a => a.Category)
        //    //    .Include(a => a.Sender).Where(m => m.Id.Equals(id));
        //        //.FirstOrDefault(m => m.Id.Equals(id));


        //}
    }
}
