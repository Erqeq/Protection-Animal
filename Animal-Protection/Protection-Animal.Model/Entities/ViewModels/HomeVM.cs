using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protection_Animal.Model.Entities.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<ApplicationCategory> Categories { get; set; }
        public IEnumerable<Application> Applications { get; set; }
        public IEnumerable<Client> Clients { get; set; }    

    }
}
