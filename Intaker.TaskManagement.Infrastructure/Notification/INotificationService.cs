namespace Intaker.TaskManagement.Infrastructure.Notification
{
    public interface INotificationService
    {
        Task Notify(string message);
    }
}
