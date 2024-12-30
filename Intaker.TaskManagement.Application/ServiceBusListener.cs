using Intaker.TaskManagement.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Intaker.TaskManagement.Application
{
    public class ServiceBusListener : BackgroundService
    {
        private readonly ServiceBusHandler _serviceBusHandler;

        public ServiceBusListener(IServiceProvider serviceProvider)
        {
            _serviceBusHandler = serviceProvider.CreateScope()
                .ServiceProvider.GetService<ServiceBusHandler>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _serviceBusHandler.ProcessMessages();
            }
        }
    }
}
