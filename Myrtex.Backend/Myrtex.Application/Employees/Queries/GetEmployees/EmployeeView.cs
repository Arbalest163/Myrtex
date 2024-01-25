using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Domain;

namespace Myrtex.Application.Employees.Queries.GetEmployees;

public class EmployeeView : IMapWith<Employee>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string BirthDate { get; set; }
    public string Department { get; set; }
    public string DateOfEmployment { get; set; }
    public string Salary { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Employee, EmployeeView>()
            .ForMember(e => e.FirstName, opt => opt.MapFrom(e => e.FirstName))
            .ForMember(e => e.LastName, opt => opt.MapFrom(e => e.LastName))
            .ForMember(e => e.MiddleName, opt => opt.MapFrom(e => e.MiddleName))
            .ForMember(e => e.BirthDate, opt => opt.MapFrom(e => e.BirthDate.ToShortDateString()))
            .ForMember(e => e.Department, opt => opt.MapFrom(e => e.Department.Name))
            .ForMember(e => e.DateOfEmployment, opt => opt.MapFrom(e => e.InformationEmployment.DateOfEmployment.ToShortDateString()))
            .ForMember(e => e.Salary, opt => opt.MapFrom(e => e.Salary))
            ;
    }
}