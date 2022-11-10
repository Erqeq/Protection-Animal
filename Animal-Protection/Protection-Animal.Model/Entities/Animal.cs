using System.ComponentModel.DataAnnotations;

namespace Protection_Animal.Model.Entities
{
    public class Animal : BaseEntity
    {
        public DateTime DateOfBirth { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(1000)]
        public string Image { get; set; }
    }
}
