using Intaker.TaskManagement.Infrastructure.Models;

namespace Intaker.TaskManagement.Infrastructure.Queue
{
    public interface IQueueService
    {
        Task Enqueue(QueueAction action, object data);
    }
}
