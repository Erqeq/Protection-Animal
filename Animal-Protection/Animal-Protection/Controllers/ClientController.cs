using Animal_Protection.Areas.Identity.Data;
using Animal_Protection.Data;
using Protection_Animal.Model.Entities;

namespace Protection_Animal.Controller
{
    public class ClientController
    {
        private readonly AppDbContext _dbContext;
        public ClientController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void AddUser(AnimalProtectionUser user)
        {
            var animalProtectionUser = MapUser(user);
            _dbContext.Clients.Add(animalProtectionUser);
            _dbContext.SaveChanges();
        }
        private Client MapUser(AnimalProtectionUser user)
        {
            return new Client()
            {
                Id = user.Id,
                Name = user.FullName,
                Email = user.Email,
                TelephoneNumber = user.PhoneNumber
            };
        }
    }
}

