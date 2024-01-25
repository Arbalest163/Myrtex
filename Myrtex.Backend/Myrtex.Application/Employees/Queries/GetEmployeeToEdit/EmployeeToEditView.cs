using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Domain;

namespace Myrtex.Application.Employees.Queries.GetEmployeeToEdit;

public class EmployeeToEditView : IMapWith<Employee>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public int DepartmentId { get; set; }
    public string DateOfEmployment { get; set; }
    public string Salary { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeToEditView>()
            .ForMember(e => e.FirstName, opt => opt.MapFrom(e => e.FirstName))
            .ForMember(e => e.LastName, opt => opt.MapFrom(e => e.LastName))
            .ForMember(e => e.MiddleName, opt => opt.MapFrom(e => e.MiddleName))
            .ForMember(e => e.DepartmentId, opt => opt.MapFrom(e => e.Department.Id))
            .ForMember(e => e.DateOfEmployment, opt => opt.MapFrom(e => e.InformationEmployment.DateOfEmployment.ToShortDateString()))
            .ForMember(e => e.Salary, opt => opt.MapFrom(e => e.Salary))
            ;
    }
}
