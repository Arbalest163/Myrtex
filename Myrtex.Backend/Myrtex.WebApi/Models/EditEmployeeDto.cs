using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Application.Employees.Commands.EditEmployee;

namespace Myrtex.WebApi.Models;

public class EditEmployeeDto : IMapWith<EditEmployeeCommand>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MidleName { get; set; }
    public DateTime BirthDate { get; set; }
    public int DepartmentId { get; set; }
    public decimal Salary { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EditEmployeeDto, EditEmployeeCommand>();
    }
}
