using AutoMapper;
using Intaker.TaskManagement.Domain.Services;
using MediatR;

namespace Intaker.TaskManagement.Application.Queries.GetAllTasksQuery
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<Domain.Models.Task>>
    {
        private readonly IRepository<Data.Models.Task> _repository;
        private readonly IMapper _mapper;

        public GetAllTasksQueryHandler(IRepository<Data.Models.Task> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Domain.Models.Task>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var dbTasks = await _repository.GetAll();
            return _mapper.Map<IEnumerable<Domain.Models.Task>>(dbTasks);
        }
    }
}
