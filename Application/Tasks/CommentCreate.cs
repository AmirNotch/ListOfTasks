using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tasks;

public class CommentCreate
{
    public class Command : IRequest<Result<Unit>>
    {
        public Comment Comment { get; set; }
        public string Id { get; set; }
    }
        
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.Comment).SetValidator(new CommentValidator());
        }
    }
        
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IUserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }
            
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {

            var user = await _context.Tasks.Where(x => 
                x.Id.ToString() == request.Id).FirstOrDefaultAsync();

            var attendee = new Comment
            {
                Task = user,
                Body = request.Comment.Body,
                CreatedAt = DateTime.UtcNow,
            };
                
            //request.ListTaskDto.Add(attendee);
                
            _context.Comments.Add(attendee);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to create Comment");
            }
                
            return Result<Unit>.Success(Unit.Value);
        }
    }
}