namespace MVC_CongratulationApplication.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ActivationCode { get; set; }
        private TimeOnly SendingTime { get; set; }
        public bool isAllowSending { get; set; }
    }
}
