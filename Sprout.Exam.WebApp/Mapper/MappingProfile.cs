using AutoMapper;
using Sprout.Exam.DataAccess;
using Sprout.Exam.WebApp.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Mapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Employee, EmployeeDto>()
				.ForMember(dest => dest.TypeId, act => act.MapFrom(src => src.EmployeeTypeId))
				.ForMember(dest => dest.Birthdate, act => act.MapFrom(src => src.Birthdate.ToString("yyyy-MM-dd")))
				.ReverseMap();
			CreateMap<EditEmployee, EditEmployeeDto>()
				.ForMember(dest => dest.TypeId, act => act.MapFrom(src => src.EmployeeTypeId))
				.ReverseMap();

		}
	}
}
