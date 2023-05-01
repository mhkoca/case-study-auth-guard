using AutoMapper;
using CaseStudy.Employee.API.Models;
using CaseStudy.Employee.API.Models.DTOs;
using CaseStudy.Employee.API.Models.Entities;

namespace CaseStudy.Service.MapProfiles
{
    public class EmployeeProfile : Profile
	{
		public EmployeeProfile()
		{
			CreateMap<EmployeeEntity, EmployeeDTO>();
			CreateMap<EmployeeDTO, EmployeeEntity>();
			CreateMap<EmployeeCreateRequestModel, EmployeeEntity>();
		}
	}
}
