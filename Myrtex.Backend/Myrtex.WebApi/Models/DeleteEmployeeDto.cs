using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Application.Employees.Commands.DeleteEmployee;

namespace Myrtex.WebApi.Models;

public class DeleteEmployeeDto : IMapWith<DeleteEmployeeCommand>
{
    public int Id { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DeleteEmployeeCommand, DeleteEmployeeDto>();
    }
}
