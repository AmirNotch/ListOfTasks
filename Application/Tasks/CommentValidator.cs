using Domain;
using FluentValidation;

namespace Application.Tasks;

public class CommentValidator : AbstractValidator<Comment>
{
    public CommentValidator()
    {
        RuleFor(x => x.Body).NotEmpty();
    }
}