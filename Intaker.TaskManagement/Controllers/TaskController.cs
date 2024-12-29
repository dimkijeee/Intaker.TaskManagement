using Intaker.TaskManagement.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Intaker.TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService TaskService;

        public TaskController(ITaskService _taskService)
        {
            TaskService = _taskService;
        }

        [HttpGet]
        public async Task<IEnumerable<Domain.Models.Task>> Get()
        {
            return await TaskService.GetAll();
        }

        [HttpPost]
        public async Task<Domain.Models.Task> Post([FromBody] Domain.Models.Task task)
        {
            return await TaskService.Create(task);
        }

        [HttpPut("{id}")]
        public async Task<Domain.Models.Task> Put(int id, [FromBody] Domain.Models.Status newStatus)
        {
            return await TaskService.UpdateStatus(id, newStatus);
        }
    }
}
