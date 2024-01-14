using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Domain;

namespace Myrtex.Application.Employees.Queries.GetEmployees;

public class EmployeeView : IMapWith<Employee>
{
    public string Name { get; set; }
    public string Department { get; set; }
    public string DateOfEmployment { get; set; }
    public string Salary { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeView>()
            .ForMember(e => e.Name, opt => opt.MapFrom(e => $"{e.FirstName} {e.LastName} {e.MiddleName}".Trim()))
            .ForMember(e => e.Department, opt => opt.MapFrom(e => e.Department.Name))
            .ForMember(e => e.DateOfEmployment, opt => opt.MapFrom(e => e.InformationEmployment.DateOfEmployment.ToShortDateString()))
            .ForMember(e => e.Salary, opt => opt.MapFrom(e => e.Salary))
            ;
    }
}