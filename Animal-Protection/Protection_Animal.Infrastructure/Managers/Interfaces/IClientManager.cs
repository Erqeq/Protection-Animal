using Protection_Animal.Model.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protection_Animal.Infrastructure.Managers.Interfaces
{
    public interface IClientManager
    {
        public List<Client> GetAll();
        public Client GetById(string id);
        public Client Delete(string id);
        public Client Create(Client client);


    }
}
