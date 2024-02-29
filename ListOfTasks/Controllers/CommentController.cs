using Domain;
using ListOfTasks.Filter;
using ListOfTasks.Helpers;
using ListOfTasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace ListOfTasks.Controllers;

public class CommentController : BaseApiController
{
    private readonly DataContext context;
    private readonly IUriService uriService;

    public CommentController(DataContext context, IUriService uriService)
    {
        this.context = context;
        this.uriService = uriService;
    }

    [HttpGet]
    public async Task<ActionResult> GetListOfComments([FromQuery] PaginationFilter filter)
    {
        var route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.Comments
            .Where(t => t.Task.Id.ToString() == filter.Id)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var totalRecords = await context.Tasks.CountAsync();
        var pagedReponse =
            PaginationHelper.CreatePagedReponse<Comment>(pagedData, validFilter, totalRecords, uriService, route);
        return Ok(pagedReponse);
    }

    [HttpGet("search/{searchName}")]
    public async Task<ActionResult> GetListOfComments([FromQuery] PaginationFilter filter, string searchName)
    {
        var route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.Comments
            .Where(t => t.Task.Id.ToString() == filter.Id)
            .Where(o => o.Body.ToLower().Contains(searchName.Trim().ToLower()))
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var totalRecords = await context.Tasks.CountAsync();
        var pagedReponse =
            PaginationHelper.CreatePagedReponse<Comment>(pagedData, validFilter, totalRecords, uriService, route);
        return Ok(pagedReponse);
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> CreateComment([FromBody] Comment comment, string id)
    {
        return Ok(await Mediator.Send(new CommentCreate.Command { Comment = comment, Id = id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditComment(Guid id, [FromBody] Comment comment)
    {
        comment.Id = id;
        return Ok(await Mediator.Send(new EditComment.Command { Comment = comment }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(Guid id)
    {
        return Ok(await Mediator.Send(new DeleteComment.Command { Id = id }));
    }
}