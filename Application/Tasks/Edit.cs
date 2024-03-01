using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Tasks;

public class Edit
{
    public class Command : IRequest<Result<Unit>>
    {
        public ListTask ListTask { get; set; }
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
            var activity = await _context.ListTasks.FindAsync(request.ListTask.Id);

            if (activity == null)
            {
                return null;
            }

            _mapper.Map(request.ListTask, activity);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update activity");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}