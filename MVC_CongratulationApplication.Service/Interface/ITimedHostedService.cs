namespace MVC_CongratulationApplication.Service.Interface
{
    public interface ITimedHostedService
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
        void Dispose();
        Task SendEmail();
        void CheckingAndSending(object obj);
        void Initialize(string Email, string Message, string Subject);

    }
}
