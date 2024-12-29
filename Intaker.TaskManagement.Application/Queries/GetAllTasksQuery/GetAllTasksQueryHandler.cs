using MediatR;

namespace Intaker.TaskManagement.Application.Queries.GetAllTasksQuery
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<Domain.Models.Task>>
    {
        public Task<IEnumerable<Domain.Models.Task>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
