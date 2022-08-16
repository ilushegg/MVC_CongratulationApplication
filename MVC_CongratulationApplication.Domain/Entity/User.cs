using System.ComponentModel.DataAnnotations;

namespace MVC_CongratulationApplication.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните имя")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Имя должно быть не менее 3 и не более 40 символов")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Заполните email")]
        [EmailAddress(ErrorMessage = "Неккоректный email")]
        public string Email { get; set; }
        public string? ActivationCode { get; set; }
        [Required(ErrorMessage = "Укажите время")]
        public DateTime SendingTime { get; set; }
        public bool isAllowSending { get; set; }
    }
}
