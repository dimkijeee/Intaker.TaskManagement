using Azure.Messaging.ServiceBus;
using Intaker.TaskManagement.Domain.Infrastructure;
using Intaker.TaskManagement.Infrastructure.Models;

namespace Intaker.TaskManagement.Application.Services
{
    public class QueueService : IQueueService
    {
        private readonly ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _serviceBusSender;

        public QueueService(ServiceBusClient serviceBusClient, string queueName)
        {
            _serviceBusClient = serviceBusClient;
            _serviceBusSender = _serviceBusClient.CreateSender(queueName);
        }

        public async Task Enqueue(QueueAction action, object data)
        {
            try
            {
                await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(
                    new QueueMessage(action, data).ToString()));
            }
            finally
            {
                await _serviceBusSender.DisposeAsync();
                await _serviceBusClient.DisposeAsync();
            }
        }
    }
}
