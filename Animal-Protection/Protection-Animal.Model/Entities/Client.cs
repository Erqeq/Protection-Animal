using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Protection_Animal.Model.Entities
{
    public class Client : BaseEntity

    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [Range(1, 12,ErrorMessage = "Incorrect number")]
        public int TelephoneNumber { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
    }
}
