using Azure.Messaging.ServiceBus;
using Intaker.TaskManagement.Application.Services;
using Intaker.TaskManagement.Infrastructure.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Intaker.TaskManagement.Functions
{
    public class TaskDequeue
    {
        private readonly ILogger<TaskDequeue> _logger;
        private readonly TaskActionsMessageHandler _taskActionsMessageHandler;

        public TaskDequeue(ILogger<TaskDequeue> logger, TaskActionsMessageHandler taskActionsMessageHandler)
        {
            _logger = logger;
            _taskActionsMessageHandler = taskActionsMessageHandler;
        }

        [Function(nameof(TaskDequeue))]
        public async Task Run(
            [ServiceBusTrigger("intaker-tm-tasks", Connection = "ServiceBusConnectionString", AutoCompleteMessages = false)]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            await _taskActionsMessageHandler.HandleMessage(
                JsonConvert.DeserializeObject<QueueMessage>(message.Body.ToString()));

            await messageActions.CompleteMessageAsync(message);
        }
    }
}
