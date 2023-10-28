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
			CreateMap<Employee, EmployeeDto>().ReverseMap();
		}
	}
}
