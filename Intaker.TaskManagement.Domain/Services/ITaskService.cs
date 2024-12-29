using Intaker.TaskManagement.Domain.Models;

namespace Intaker.TaskManagement.Domain.Services
{
    public interface ITaskService
    {
        Task<List<Models.Task>> GetAll();
        Task<Models.Task> Create(Models.Task task);
        Task<Models.Task> UpdateStatus(int id, Status newStatus);
    }
}
