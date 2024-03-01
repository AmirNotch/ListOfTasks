using Application.Core;
using AutoMapper;
using MediatR;
using Persistence;

namespace Application.Tasks;

public class ChangeTast
{
    public class Command : IRequest<Result<Unit>>
    {
        public Guid FirstID { get; set; }
        public Guid SecondID { get; set; }
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
            var activity = await _context.Tasks.FindAsync(request.FirstID);
                
            var anotherListTask = await _context.ListTasks.FindAsync(request.SecondID);

            if (activity == null)
            {
                return null;
            }
                
            if (anotherListTask == null)
            {
                return null;
            }
                
            var changed = new Domain.Tasks
            {
                ListTask = anotherListTask,
                Name = activity.Name,
                Description = activity.Description,
                Status = activity.Status
            };
                
            _mapper.Map(changed, activity);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to update activity");
            }

            return Result<Unit>.Success(Unit.Value);
        }
    }
}