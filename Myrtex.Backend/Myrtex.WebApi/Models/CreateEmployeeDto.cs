using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Application.Employees.Commands.CreateEmployee;

namespace Myrtex.WebApi.Models;

public class CreateEmployeeDto : IMapWith<CreateEmployeeCommand>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public int DepartmentId { get; set; }
    public decimal Salary { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateEmployeeDto, CreateEmployeeCommand>()
            .ForMember(e => e.FirstName, opt => opt.MapFrom(e => e.FirstName))
            .ForMember(e => e.LastName, opt => opt.MapFrom(e => e.LastName))
            .ForMember(e => e.MiddleName, opt => opt.MapFrom(e => e.MiddleName))
            .ForMember(e => e.DepartmentId, opt => opt.MapFrom(e => e.DepartmentId))
            .ForMember(e => e.BirthDate, opt => opt.MapFrom(e => e.BirthDate))
            .ForMember(e => e.Salary, opt => opt.MapFrom(e => e.Salary))
            ;
    }
}
