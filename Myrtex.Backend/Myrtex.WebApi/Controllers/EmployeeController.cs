using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Myrtex.Application.Common.Models;
using Myrtex.Application.Employees.Commands.CreateEmployee;
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

    /// <summary>
    /// Получить сотрудника по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор сотрудника</param>
    /// <returns></returns>
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<EmployeeToEditView>> Get([FromRoute]int id)
    {
        var query = new GetEmployeeToEditQuery
        {
            Id = id
        };
        var employeeView = await _mediator.Send(query);
        return Ok(employeeView);
    }

    /// <summary>
    /// Получить постраничный список сотрудников
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<PageResponse<EmployeeView>>> Get([FromBody]GetEmployesQuery query)
    {
        var pageResponse = await _mediator.Send(query);
        return Ok(pageResponse);
    }

    /// <summary>
    /// Добавить сотрудника
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<int>> AddEmployee([FromBody]CreateEmployeeDto request)
    {
        var command = _mapper.Map<CreateEmployeeCommand>(request);
        var employeeId = await _mediator.Send(command);
        return Ok(employeeId);
    }

    /// <summary>
    /// Редактировать сотрудника
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    [Route("edit")]
    public async Task<ActionResult> EditEmployee([FromBody]EditEmployeeDto request)
    {
        var command = _mapper.Map<EditEmployeeCommand>(request);
        await _mediator.Send(command);
        return Ok();
    }

    /// <summary>
    /// Удалить сотрудника
    /// </summary>
    /// <param name="id">Идентификатор отрудника</param>
    /// <returns></returns>
    [HttpDelete]
    [Route("delete/{id}")]
    public async Task<ActionResult> DeleteEmployee([FromRoute]int id)
    {
        var command = new DeleteEmployeeCommand
        {
            Id = id
        };
        await _mediator.Send(command);
        return Ok();
    }
}