using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectAnimal.Model.Repository;
using Protection_Animal;
using Protection_Animal.Model.Entities;

namespace Protection_Animal.Controller
{
    public class ApllicationCategoryController
    {
        private IRepository<ApplicationCategory> _repository;
        public ApllicationCategoryController(IRepository<ApplicationCategory> repository)
        {
            _repository = repository;
        }
        public ApplicationCategory Create(ApplicationCategory applicationCategory)
        {
            return _repository.Create(applicationCategory);
        }

    }
}
