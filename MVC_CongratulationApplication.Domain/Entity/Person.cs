using System.ComponentModel.DataAnnotations;

namespace MVC_CongratulationApplication.Domain.Entity
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните дату рождения")]
        public DateTime Birthday { get; set; }
        public string? Filename { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
