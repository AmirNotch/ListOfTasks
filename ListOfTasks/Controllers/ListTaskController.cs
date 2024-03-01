using Application.Tasks;
using Domain;
using ListOfTasks.Filter;
using ListOfTasks.Helpers;
using ListOfTasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace ListOfTasks.Controllers;

public class ListTaskController : BaseApiController
{
    private readonly DataContext context;
    private readonly IUriService uriService;

    public ListTaskController(DataContext context, IUriService uriService)
    {
        this.context = context;
        this.uriService = uriService;
    }

    [HttpGet]
    public async Task<ActionResult> GetListOfTask([FromQuery] PaginationFilter filter)
    {
        var route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.ListTasks
            .Where(l => l.AppUser.Id == filter.Id)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();
        var totalRecords = await context.ListTasks.CountAsync();
        var pagedReponse =
            PaginationHelper.CreatePagedReponse<ListTask>(pagedData, validFilter, totalRecords, uriService, route);
        return Ok(pagedReponse);
    }

    [HttpGet("search/{searchName}")]
    public async Task<ActionResult> GetListOfTask([FromQuery] PaginationFilter filter, string searchName)
    {
        var route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.ListTasks
            .Where(l => l.AppUser.Id == filter.Id)
            .Where(o => o.Name.ToLower().Contains(searchName.Trim().ToLower()))
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();
        var totalRecords = await context.ListTasks.CountAsync();
        var pagedReponse =
            PaginationHelper.CreatePagedReponse<ListTask>(pagedData, validFilter, totalRecords, uriService, route);
        return Ok(pagedReponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditListTast(Guid id, [FromBody] ListTask listTask)
    {
        listTask.Id = id;
        return Ok(await Mediator.Send(new Edit.Command { ListTask = listTask }));
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> CreateListTast([FromBody] ListTask listTask, string id)
    {
        return Ok(await Mediator.Send(new Create.Command { ListTask = listTask, Id = id }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteListTast(Guid id)
    {
        return Ok(await Mediator.Send(new Delete.Command { Id = id }));
    }
}