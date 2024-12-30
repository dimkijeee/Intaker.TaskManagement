using MediatR;

namespace Intaker.TaskManagement.Application.Commands.CreateTaskCommand
{
    public class CreateTaskCommand : IRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string AssignedTo { get; set; }
    }
}
