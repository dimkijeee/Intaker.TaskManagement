using AutoMapper;
using Intaker.TaskManagement.Application.Commands.CreateTaskCommand;

namespace Intaker.TaskManagement.Application.Dependencies
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.Task, Data.Models.Task>().ReverseMap();
            CreateMap<CreateTaskCommand, Data.Models.Task>();
        }
    }
}
