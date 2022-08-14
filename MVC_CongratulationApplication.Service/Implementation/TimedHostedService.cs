using Microsoft.Extensions.Hosting;
using MVC_CongratulationApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_CongratulationApplication.Service.Implementation
{
    public class TimedHostedService : IHostedService, IDisposable, ITimedHostedService
    {
        private ISendService _sendService;
        private Timer _timer;

        public TimedHostedService(ISendService sendService)
        {
            _sendService = sendService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(new TimerCallback(_sendService.CheckingAndSending), null, 0, 10000);

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
    }
}
