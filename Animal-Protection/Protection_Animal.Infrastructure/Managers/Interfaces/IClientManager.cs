using Protection_Animal.Model.Entities;

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
