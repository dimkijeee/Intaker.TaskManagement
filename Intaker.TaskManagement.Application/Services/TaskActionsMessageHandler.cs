using AutoMapper;
using Intaker.TaskManagement.Application.Commands.CreateTaskCommand;
using Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand;
using Intaker.TaskManagement.Domain.Services;
using Intaker.TaskManagement.Infrastructure.Models;
using Intaker.TaskManagement.Infrastructure.Notification;
using Newtonsoft.Json.Linq;

namespace Intaker.TaskManagement.Application.Services
{
    public class TaskActionsMessageHandler
    {
        private readonly IRepository<Data.Models.Task> _repository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        private const string SuccessMessageFormat = "{0}: Success";
        private const string ErrorMessageFormat = "{0}: Error";

        public TaskActionsMessageHandler(IRepository<Data.Models.Task> repository, INotificationService notificationService, IMapper mapper)
        {
            _repository = repository;
            _notificationService = notificationService;
            _mapper = mapper;
        }

        public async Task HandleMessage(QueueMessage message)
        {
            try
            {
                switch (message.Action)
                {
                    case QueueAction.CreateTask:
                        await CreateTask((message.Data as JObject).ToObject<CreateTaskCommand>());
                        break;
                    case QueueAction.UpdateTaskStatus:
                        await UpdateTaskStatus((message.Data as JObject).ToObject<UpdateTaskStatusCommand>());
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Action is not supported");
                }
            }
            catch
            {
                await _notificationService.Notify(string.Format(ErrorMessageFormat, message.Action.ToString()));
            }

            await _notificationService.Notify(string.Format(SuccessMessageFormat, message.Action.ToString()));
        }

        private async Task CreateTask(CreateTaskCommand command)
        {
            var dbTask = _mapper.Map<Data.Models.Task>(command);

            await _repository.Create(dbTask);
            _repository.Save();
        }

        private async Task UpdateTaskStatus(UpdateTaskStatusCommand command)
        {
            var dbTask = await _repository.Get(command.Id);

            if (dbTask != null)
            {
                dbTask.Status = (int)command.NewStatus;

                await _repository.Update(dbTask);
                _repository.Save();
            }
        }
    }
}
