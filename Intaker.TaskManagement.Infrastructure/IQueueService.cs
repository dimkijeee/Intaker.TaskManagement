using Intaker.TaskManagement.Infrastructure.Models;

namespace Intaker.TaskManagement.Domain.Infrastructure
{
    public interface IQueueService
    {
        Task Enqueue(QueueAction action, object data);
    }
}
