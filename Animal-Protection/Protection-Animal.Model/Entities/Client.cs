using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protection_Animal.Model.Entities
{
    public class Client : BaseEntity<Client, string>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string TelephoneNumber { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; } = "Bishkek";
        public List<Application> Applications { get; set; }
    }
}
