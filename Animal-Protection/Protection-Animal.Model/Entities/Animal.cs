using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protection_Animal.Model.Entities
{
    public class Animal : BaseEntity<Animal, int>
    {
        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(1000)]
        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
