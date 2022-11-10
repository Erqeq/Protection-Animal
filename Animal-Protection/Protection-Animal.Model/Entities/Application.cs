using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protection_Animal.Model.Entities
{
    public class Application : BaseEntity
    {
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; }  
        public int CategoryId { get; set; }
        public ApplicationCategory Category { get; set; }
        public int SenderId { get; set; }
        public Client Sender { get; set; }  
        public int ReceiverId { get; set; }
        public Client Receiver { get; set; }
        public int AnimalId { get; set; }   
        public Animal Animal { get; set; }  
    }
}
