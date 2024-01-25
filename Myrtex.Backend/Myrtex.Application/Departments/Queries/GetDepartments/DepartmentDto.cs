using AutoMapper;
using Myrtex.Application.Common.Mappings;
using Myrtex.Domain;

namespace Myrtex.Application.Departments.Queries.GetDepartments;

public class DepartmentDto : IMapWith<Department>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, DepartmentDto>()
            ;
    }
}
