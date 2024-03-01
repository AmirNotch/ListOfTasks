﻿using Application.Core;
using Application.Interfaces;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Tasks;

public class Create
{
    public class Command : IRequest<Result<Unit>>
    {
        public ListTask ListTask { get; set; }
        public string Id { get; set; }
    }
        
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.ListTask).SetValidator(new ListTaskValidator());
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

            var user = await _context.AppUsers.Where(x => 
                x.Id == request.Id).FirstOrDefaultAsync();

            var attendee = new ListTask
            {
                AppUser = user,
                Name = request.ListTask.Name,
                Description = request.ListTask.Description
            };
                
            //request.ListTaskDto.Add(attendee);
                
            _context.ListTasks.Add(attendee);

            var result = await _context.SaveChangesAsync() > 0;

            if (!result)
            {
                return Result<Unit>.Failure("Failed to create ListTask");
            }
                
            return Result<Unit>.Success(Unit.Value);
        }
    }
}