using Domain;
using FluentValidation;

namespace Application.Tasks;

public class ListTaskValidator : AbstractValidator<ListTask>
{
    public ListTaskValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
    }
}