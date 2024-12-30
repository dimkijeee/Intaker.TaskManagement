using Intaker.TaskManagement.Application.Commands.CreateTaskCommand;
using Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand;
using Intaker.TaskManagement.Application.Queries.GetAllTasksQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Intaker.TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain.Models.Task>>> Get()
        {
            return Ok(await _mediator.Send(new GetAllTasksQuery()));
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateTaskCommand command)
        {
            await _mediator.Send(command);
            return Accepted();
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateTaskStatusCommand command)
        {
            await _mediator.Send(command);
            return Accepted();
        }
    }
}
