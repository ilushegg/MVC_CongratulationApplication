using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MVC_CongratulationApplication.Service.Interface;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class SendService : ISendService
    {
        private readonly IConfiguration _configuration;
        private readonly IContainer _container;

        public SendService(IConfiguration configuration, IContainer container)
        {
            _configuration = configuration;
            _container = container;
            
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
                    await client.AuthenticateAsync("congrats.yourfriend@bk.ru", "*********");
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);
                }
                catch(Exception ex)
                {

                }
            }
            Console.WriteLine("Письмо отправлено");
        }

        public async Task<string> GenerateMessage()
        {
            var birthdayPeople = await _container.GetBirthdayPeople();
            if(birthdayPeople.StatusCode == Domain.Enum.StatusCode.PeopleNotFound)
            {
                return "NotFound";
            }
            string message = "Не забудьте поздравить друзей!\n" +
                "У ваших друзей намечается день рождения:\n";
            foreach(var person in birthdayPeople.Data)
            {
                message += person.Name + "\t" + person.Birthday.Date + "\n";
            }
            message += "\n\n\nВы получили данное сообщение так как когда-то указали свой электронный адрес в приложении для поздравления друзей, знакомых, товарищей.";
            return message;
        }

        public void CheckingAndSending(object obj)
        {
            var response = GenerateMessage();
            if(response.Result != "NotFound")
            {
                var userResponse = _container.GetUser();
                Task send = SendEmail(userResponse.Result.Data.Email, "Поздравь друзей!", response.Result);
            }

        }


        
    }
}
