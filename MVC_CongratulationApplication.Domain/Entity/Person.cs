using System.ComponentModel.DataAnnotations;

namespace MVC_CongratulationApplication.Domain.Entity
{
    public class Person
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните имя")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Имя должно быть не менее 3 и не более 40 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Заполните дату рождения")]
        [RegularExpression(@"((19|20)\d\d)-\d\d-\d\d", ErrorMessage = "Некорректная дата")]
        public DateTime Birthday { get; set; }
        public string? Filename { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
