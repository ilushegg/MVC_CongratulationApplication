using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MimeKit;
using MVC_CongratulationApplication.DAL.Data;
using MVC_CongratulationApplication.Service.Interface;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class TimedHostedService : IHostedService, IDisposable, ITimedHostedService
    {

        public string Email { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public DateTime Time { get; set; }
        public bool Send = false;
        public bool IsAllow = true;

        private readonly IConfiguration _configuration;
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;

        public TimedHostedService(IServiceProvider serviceProvider, IConfiguration configuration, IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _scopeFactory = serviceScopeFactory;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(new TimerCallback(CheckingAndSending), null, 30000, 60000);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }


        private async Task Initialize()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

                string message = "";
                var birthdayPeople = dbContext.People.Where(p => p.Birthday.Month == DateTime.Now.Month && p.Birthday.Day > DateTime.Now.Day && p.Birthday.Day < DateTime.Now.Day + 7);

                if (birthdayPeople.ToList() != null)
                {
                    message = "Не забудьте поздравить друзей!\n\n" +
                    "У ваших друзей намечается день рождения:\n\n";
                }
                else
                {
                    message = "NotFound";
                }

                foreach (var person in birthdayPeople)
                {
                    message += "\t" + person.Name + "\t" + person.Birthday.Date.ToString("dd.MM.yyyy") + "\n";
                }
                message += "\n\n\nВы получили данное сообщение так как когда-то указали свой электронный адрес в приложении для поздравления друзей, знакомых, товарищей.";
                var user = dbContext.Users.FirstOrDefault();
                if (user != null)
                {
                    Message = message;
                    Email = user.Email;
                    Time = user.SendingTime;
                    IsAllow = user.isAllowSending;
                    Send = false;
                }
            }

            Subject = "Поздравь друзей!";
        }

        public void Initialize(string Email, string Message, string Subject)
        {
            this.Email = Email;
            this.Message = Message;
            this.Subject = Subject;
        }


        public Timer timer { get; set; }
        public async Task SendEmail()
        {
            if (Email != null)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Congratulation Application", _configuration["Mail:Email"]));
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
                        await client.AuthenticateAsync(_configuration["Mail:Email"], _configuration["Mail:Password"]);
                        await client.SendAsync(emailMessage);
                        await client.DisconnectAsync(true);
                        Send = true;
                        Console.WriteLine("Письмо отправлено");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

        }

        public void CheckingAndSending(object obj)
        {
            Initialize();
            if (Message != "NotFound")
            {
                if (!Send && IsAllow && Time.Hour == DateTime.Now.Hour && Time.Minute == DateTime.Now.Minute)
                {
                    Subject = "Поздравь друзей!";
                    Task send = SendEmail();
                }
            }

        }
    }
}
