using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Tasks;

public class EditTask
{
    public class Command : IRequest<Result<Unit>>
    {
        public Domain.Tasks Task { get; set; }
        public string Id { get; set; }
    }
        
    public class Handler : IRequestHandler<Command, Result<Unit>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public Handler(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
            
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await _context.Tasks.FindAsync(request.Task.Id);

            if (activity == null)
            {
                return null;
            }
                
            request.Task.CreatedAt = DateTime.UtcNow;
            _mapper.Map(request.Task, activity);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update activity");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}