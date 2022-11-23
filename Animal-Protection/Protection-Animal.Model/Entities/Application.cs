using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protection_Animal.Model.Entities
{
    public class Application : BaseEntity
    {
        [MaxLength(500)]
        public string Description { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }
        public ApplicationCategory Category { get; set; }
        public string SenderId { get; set; } 
        public Client Sender { get; set; }
        [DisplayName("Animal Type")]
        public int AnimalId { get; set; }   
        public Animal Animal { get; set; }
        [MaxLength(1000)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
