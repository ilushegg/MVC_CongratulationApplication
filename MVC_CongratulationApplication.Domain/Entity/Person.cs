namespace MVC_CongratulationApplication.Domain.Entity
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public string? Filename { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
