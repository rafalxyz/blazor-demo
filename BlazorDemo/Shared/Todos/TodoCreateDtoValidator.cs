using BlazorDemo.Shared.Services;
using FluentValidation;

namespace BlazorDemo.Shared.Todos;

public class TodoCreateDtoValidator : ValidatorBase<TodoCreateDto>
{
    public TodoCreateDtoValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Please provide name.")
            .MinimumLength(3).WithMessage("Name should be at least 3 characters long.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Please provide due date")
            .Must(dueDate => dueDate >= dateTimeProvider.Now()).WithMessage("Due date must be not before today.");

        RuleFor(x => x.Priority)
            .IsInEnum().WithMessage("Please provide valid priority");
    }
}
