using FluentValidation;

namespace Application.Tasks;

public class TaskValidator : AbstractValidator<Domain.Tasks>
{
    public TaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.CreatedAt).NotEmpty();
    }
}