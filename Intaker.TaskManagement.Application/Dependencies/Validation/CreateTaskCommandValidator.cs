using FluentValidation;
using Intaker.TaskManagement.Application.Commands.CreateTaskCommand;
using Intaker.TaskManagement.Domain.Models;

namespace Intaker.TaskManagement.Application.Dependencies.Validation
{
    public sealed class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator() 
        {
            RuleFor(command => command.Name)
                .NotEmpty()
                .WithMessage("Name can not be empty.");
            RuleFor(command => command.Description)
                .NotEmpty()
                .WithMessage("Description can not be empty.");
            RuleFor(command => command.Status)
                .InclusiveBetween((int)Status.NotStarted, (int)Status.Completed)
                .WithMessage("Status must be in range (NotStarted, InProgress, Completed).");
        }
    }
}
