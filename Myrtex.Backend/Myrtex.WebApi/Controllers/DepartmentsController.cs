using MediatR;
using Microsoft.AspNetCore.Mvc;
using Myrtex.Application.Departments.Queries.GetDepartments;

namespace Myrtex.WebApi.Controllers;

[ApiController]
[Route("departments")]
public class DepartmentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DepartmentsController( IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Получить список департаментов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<DepartmentsView>> Get()
    {
        var query = new GetDepartmentsQuery();
        var departmentsView = await _mediator.Send(query);
        return Ok(departmentsView);
    }
}
