using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MVC_CongratulationApplication.Service.Interface;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class SendService : ISendService
    {
        private readonly IConfiguration _configuration;

        public SendService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Timer timer { get; set; }
        public async Task SendEmail(string emailTo, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Congratulation Application", "congrats.yourfriend@bk.ru"));
            emailMessage.To.Add(new MailboxAddress("", emailTo));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.RichText)
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync("smtp.mail.ru", 465, true);
                    await client.AuthenticateAsync("congrats.yourfriend@bk.ru", "*******");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch(Exception ex)
                {

                }
            }
            Console.WriteLine("Письмо отправлено");
        }
        public void Dispose() { }
        public void Init()
        {
            //timer = new Timer(new TimerCallback(SendEmail), null, 0, interval);
        }
    }
}
