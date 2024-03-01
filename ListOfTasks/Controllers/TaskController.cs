using Application.Tasks;
using ListOfTasks.Filter;
using ListOfTasks.Helpers;
using ListOfTasks.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace ListOfTasks.Controllers;

public class TaskController : BaseApiController
{
    public enum Status
    {
        Waiting = 0,
        Progress = 1,
        Done = 2
    }

    private readonly DataContext context;
    private readonly IUriService uriService;

    public TaskController(DataContext context, IUriService uriService)
    {
        this.context = context;
        this.uriService = uriService;
    }

    [HttpGet]
    public async Task<ActionResult> GetTasks([FromQuery] PaginationFilter filter)
    {
        var route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.Tasks
            .Where(t => t.ListTask.Id.ToString() == filter.Id)
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();

        var totalRecords = await context.Tasks.CountAsync();
        var pagedReponse =
            PaginationHelper.CreatePagedReponse<Domain.Tasks>(pagedData, validFilter, totalRecords, uriService, route);
        return Ok(pagedReponse);
    }


    [HttpGet("search/{searchName}")]
    public async Task<ActionResult> GetTasks([FromQuery] PaginationFilter filter, string searchName)
    {
        var route = Request.Path.Value;
        var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
        var pagedData = await context.Tasks
            .Where(l => l.ListTask.Id.ToString() == filter.Id)
            .Where(o => o.Name.ToLower().Contains(searchName.Trim().ToLower()))
            .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
            .Take(validFilter.PageSize)
            .ToListAsync();
        var totalRecords = await context.ListTasks.CountAsync();
        var pagedReponse =
            PaginationHelper.CreatePagedReponse<Domain.Tasks>(pagedData, validFilter, totalRecords, uriService, route);
        return Ok(pagedReponse);
    }

    // [HttpGet("searchStatus/{status}")]
    // public async Task<ActionResult> GetTasks([FromQuery]PaginationFilter filter, string status)
    // {
    //     // Status enumValue = (Status)id;
    //     // string status = enumValue.ToString(); //Visible
    //     
    //     var route = Request.Path.Value;
    //     var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
    //     var pagedData = await context.Tasks
    //         .Where(l => l.ListTask.Id.ToString() == filter.Id)
    //         .Where(o => o.Status == status)
    //         .Skip((validFilter.PageNumber - 1) * validFilter.PageSize)
    //         .Take(validFilter.PageSize)
    //         .ToListAsync();
    //     var totalRecords = await context.ListTasks.CountAsync();
    //     var pagedReponse = PaginationHelper.CreatePagedReponse<Task>(pagedData, validFilter, totalRecords, uriService, route);
    //     return Ok(pagedReponse);
    // }

    [HttpPost("{id}")]
    public async Task<IActionResult> CreateTast([FromBody] Domain.Tasks Task, string id)
    {
        return Ok(await Mediator.Send(new CreateTask.Command { Task = Task, Id = id }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditTast(Guid id, [FromBody] Domain.Tasks Task)
    {
        Task.Id = id;
        return Ok(await Mediator.Send(new EditTask.Command { Task = Task }));
    }

    [HttpPut("{id}/change/{secondID}")]
    public async Task<IActionResult> ChangeTast(Guid id, Guid secondID)
    {
        return Ok(await Mediator.Send(new ChangeTast.Command { FirstID = id, SecondID = secondID }));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTast(Guid id)
    {
        return Ok(await Mediator.Send(new TaskDelete.Command { Id = id }));
    }
}