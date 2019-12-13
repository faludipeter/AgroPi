using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgroPi.BLL.Services
{
    public class TimerService : IHostedService, IDisposable
    {
        private Timer _timer;
        private SensorsService SensorsService;

        public TimerService(SensorsService sensorsService)
        {
            SensorsService = sensorsService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }
        private void DoWork(object state)
        {
            SensorsService.SaveAllSensorData();
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
