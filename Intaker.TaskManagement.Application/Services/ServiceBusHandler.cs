using Azure.Messaging.ServiceBus;
using Intaker.TaskManagement.Infrastructure.Models;
using Microsoft.Azure.Amqp.Framing;
using Newtonsoft.Json;

namespace Intaker.TaskManagement.Application.Services
{
    public class ServiceBusHandler
    {
        private readonly TaskActionsMessageHandler _taskActionsMessageHandler;
        private readonly ServiceBusClient _serviceBusClient;

        private readonly ServiceBusSender _serviceBusSender;
        private readonly ServiceBusReceiver _serviceBusReceiver;

        public ServiceBusHandler(TaskActionsMessageHandler taskActionsMessageHandler,
            ServiceBusClient serviceBusClient, string queueName)
        {
            _taskActionsMessageHandler = taskActionsMessageHandler;
            _serviceBusClient = serviceBusClient;

            _serviceBusSender = _serviceBusClient.CreateSender(queueName);
            _serviceBusReceiver = _serviceBusClient.CreateReceiver(queueName);
        }

        public async Task SendMessage(QueueAction action, object data)
        {
            await _serviceBusSender.SendMessageAsync(new ServiceBusMessage(
                new QueueMessage(action, data).ToString()));
        }

        public async Task ProcessMessages()
        {
            var messages = _serviceBusReceiver.ReceiveMessagesAsync();

            await foreach (var message in messages)
            {
                await _taskActionsMessageHandler.HandleMessage(
                    JsonConvert.DeserializeObject<QueueMessage>(message.Body.ToString()));

                await _serviceBusReceiver.CompleteMessageAsync(message);
            }
        }
    }
}
