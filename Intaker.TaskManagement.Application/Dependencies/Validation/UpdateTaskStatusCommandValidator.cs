using FluentValidation;
using Intaker.TaskManagement.Application.Commands.UpdateTaskStatusCommand;
using Intaker.TaskManagement.Domain.Models;

namespace Intaker.TaskManagement.Application.Dependencies.Validation
{
    public sealed class UpdateTaskStatusCommandValidator : AbstractValidator<UpdateTaskStatusCommand>
    {
        public UpdateTaskStatusCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("Id is required.");
            RuleFor(command => command.NewStatus)
                .InclusiveBetween((int)Status.NotStarted, (int)Status.Completed)
                .WithMessage("Status must be in range (NotStarted, InProgress, Completed).");
        }
    }
}
