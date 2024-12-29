namespace Intaker.TaskManagement.Domain.Services
{
    public interface IQueueService
    {
        void Enqueue(string message);
        void Dequeue(string message);
    }
}
