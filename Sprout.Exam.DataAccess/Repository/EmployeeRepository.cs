﻿using Sprout.Exam.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{
		public async Task<Employee> AddAsync(CreateEmployee item)
		{
			
			throw new NotImplementedException();
		}

		public async Task<Employee> Get(int id)
		{
			Employee result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));
			return result;
		}

		public async Task<List<Employee>> GetAll()
		{
			List<Employee> employees = await Task.FromResult(StaticEmployees.ResultList);
			return employees;
		}

		public Task Remove(int id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> Update(EditEmployee item)
		{
			throw new NotImplementedException();
		}
	}
}