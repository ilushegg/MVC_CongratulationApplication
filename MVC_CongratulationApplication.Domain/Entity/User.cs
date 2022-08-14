namespace MVC_CongratulationApplication.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string Email { get; set; }
        public string? ActivationCode { get; set; } 
        public DateTime SendingTime { get; set; } 
        public bool isAllowSending { get; set; } 
    }
}
