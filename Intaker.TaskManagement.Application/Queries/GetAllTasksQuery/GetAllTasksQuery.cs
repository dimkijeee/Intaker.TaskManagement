using MediatR;

namespace Intaker.TaskManagement.Application.Queries.GetAllTasksQuery
{
    public class GetAllTasksQuery : IRequest<IEnumerable<Domain.Models.Task>>
    {
    }
}
