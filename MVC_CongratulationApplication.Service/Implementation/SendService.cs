using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MVC_CongratulationApplication.Service.Interface;
using MVC_CongratulationApplication.DAL.Interface;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class SendService : ISendService
    {
        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public DateTime Time { get; set; } 

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        
        public async Task Initialize(string Email, string Subject, string Message, DateTime Time)
        {
            this.Email = Email;
            this.Subject = Subject;
            this.Message = Message;
            this.Time = Time;
        }
        public async Task Initialize(string Email, string Subject, string Message)
        {
            this.Email = Email;
            this.Subject = Subject;
            this.Message = Message;
            this.Time = Time;
        }

        public async Task Initialize(string Email, DateTime Time)
        {
            this.Email = Email;
            this.Time = Time;
        }

        public async Task Initialize(string Message)
        {
            this.Message = Message;
        }
        public SendService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            var response = _userRepository.GetFirst();
            this.Email = response.Result.Email;
            this.Time = response.Result.SendingTime;

            
        }

        public Timer timer { get; set; }
        public async Task SendEmail()
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Congratulation Application", "congrats.yourfriend@bk.ru"));
            emailMessage.To.Add(new MailboxAddress("", Email));
            emailMessage.Subject = Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.RichText)
            {
                Text = Message
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.mail.ru", 465, true);
                    await client.AuthenticateAsync("congrats.yourfriend@bk.ru", "LJhWhEjNvXK3EiDx1w2X");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch(Exception ex)
                {

                }
            }
            Console.WriteLine("Письмо отправлено");
        }

        public void CheckingAndSending(object obj)
        {
            if (Message != "NotFound")
            {
                Console.WriteLine("/////////////////????????????????????????????????:      " + Time);
                if (Time.Hour == DateTime.Now.Hour && Time.Minute == DateTime.Now.Minute)
                {
                    Subject = "Поздравь друзей1";
                    Task send = SendEmail();
                }
            }

        }


    }
}
