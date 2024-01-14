using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Myrtex.Application.Common.Models;
using Myrtex.Application.Employees.Commands.DeleteEmployee;
using Myrtex.Application.Employees.Commands.EditEmployee;
using Myrtex.Application.Employees.Queries.GetEmployees;
using Myrtex.Application.Employees.Queries.GetEmployeeToEdit;
using Myrtex.WebApi.Models;

namespace Myrtex.WebApi.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public EmployeeController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{Id}")]
    public async Task<ActionResult<EmployeeToEditView>> Get([FromRoute]GetEmployeeToEditQuery query)
    {
        var employeeView = await _mediator.Send(query);
        return Ok(employeeView);
    }

    [HttpGet]
    public async Task<ActionResult<PageResponse<EmployeeView>>> Get([FromQuery]GetEmployesQuery query)
    {
        var pageResponse = await _mediator.Send(query);
        return Ok(pageResponse);
    }

    [HttpPost]
    public async Task<ActionResult<int>> AddEmployee([FromBody]CreateEmployeeDto request)
    {
        var command = _mapper.Map<CreateEmployeeDto>(request);
        var employeeId = await _mediator.Send(command);
        return Ok(employeeId);
    }

    [HttpPut]
    public async Task<ActionResult> EditEmployee([FromBody]EditEmployeeDto request)
    {
        var command = _mapper.Map<EditEmployeeCommand>(request);
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteEmployee([FromBody]DeleteEmployeeDto request)
    {
        var command = _mapper.Map<DeleteEmployeeCommand>(request);
        await _mediator.Send(command);
        return Ok();
    }
}