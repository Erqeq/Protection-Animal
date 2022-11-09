namespace Protection_Animal.Model.Entities
{
    public class Animal : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public string Description { get; set; }
        public string ShortDesc { get; set; }
        public string Image { get; set; }
    }
}
