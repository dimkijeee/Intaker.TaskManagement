﻿using Azure.Messaging.ServiceBus;
using Intaker.TaskManagement.Infrastructure.Models;

namespace Intaker.TaskManagement.Infrastructure.Queue
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
            }
        }
    }
}
