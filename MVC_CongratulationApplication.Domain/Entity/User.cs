using System.ComponentModel.DataAnnotations;

namespace MVC_CongratulationApplication.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Заполните имя")]
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
