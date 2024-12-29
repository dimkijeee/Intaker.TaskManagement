using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Intaker.TaskManagement.Functions
{
    public class TaskDequeue
    {
        private readonly ILogger<TaskDequeue> _logger;

        public TaskDequeue(ILogger<TaskDequeue> logger)
        {
            _logger = logger;
        }

        [Function(nameof(TaskDequeue))]
        public async Task Run(
            [ServiceBusTrigger("intaker-tm-tasks", Connection = "SBConnectionString")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
