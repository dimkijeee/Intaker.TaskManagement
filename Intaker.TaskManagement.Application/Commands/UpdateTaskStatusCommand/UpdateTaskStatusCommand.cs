using Intaker.TaskManagement.Domain.Models;
using MediatR;

namespace Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand
{
    public class UpdateTaskStatusCommand : IRequest
    {
        public int Id { get; set; }
        public int NewStatus { get; set; }
    }
}
