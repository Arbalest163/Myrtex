using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Domain;

namespace Myrtex.Application.Employees.Queries.GetEmployeeToEdit;

public class EmployeeToEditView : IMapWith<Employee>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MidleName { get; set; }
    public DepartmentToEditDto Department { get; set; }
    public DepartmentToEditDto[] AvailableDepartments { get; set; }
    public string DateOfEmployment { get; set; }
    public string Salary { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeToEditView>()
            .ForMember(e => e.FirstName, opt => opt.MapFrom(e => e.FirstName))
            .ForMember(e => e.LastName, opt => opt.MapFrom(e => e.LastName))
            .ForMember(e => e.MidleName, opt => opt.MapFrom(e => e.MiddleName))
            .ForMember(e => e.Department, opt => opt.MapFrom(e => e.Department))
            .ForMember(e => e.DateOfEmployment, opt => opt.MapFrom(e => e.InformationEmployment.DateOfEmployment.ToShortDateString()))
            .ForMember(e => e.Salary, opt => opt.MapFrom(e => e.Salary))
            ;
    }
}
