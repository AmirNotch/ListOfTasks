using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Persistence;

namespace Application.Tasks;

public class List
{
    public class Query : IRequest<Result<PagedList<ListTaskDto>>>
    {
        public ListParams Params { get; set; }
    }
        
    public class Handler : IRequestHandler<Query, Result<PagedList<ListTaskDto>>>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public Handler(DataContext context, IMapper mapper, IUserAccessor userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<PagedList<ListTaskDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.ListTasks
                .Where(l => l.AppUser.Id == request.Params.Id)
                .ProjectTo<ListTaskDto>(_mapper.ConfigurationProvider, new {currentUsername = _userAccessor.GetUsername()})
                .AsQueryable();

            // if (query.Count == 0)
            // {
            //     return NotFoundResult();
            // }

            return Result<PagedList<ListTaskDto>>.Success(
                await PagedList<ListTaskDto>.CreateAsync(query, request.Params.PageNumber,
                    request.Params.PageSize)
            );
        }
    }
}