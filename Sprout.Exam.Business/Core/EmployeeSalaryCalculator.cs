using Sprout.Exam.Business.Factory;
using Sprout.Exam.Business.Interface;
using Sprout.Exam.Business.Strategy;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Interface;
using Sprout.Exam.Common.Model;
using Sprout.Exam.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Core
{
	public class EmployeeSalaryCalculator : IEmployeeSalaryCalculator
	{
		private EmployeeStrategy _employeeStrategy;
		private IEmployeeRepository _employeeRepository;
		public EmployeeSalaryCalculator(EmployeeStrategy employeeStrategy, IEmployeeRepository employeeRepository)
		{
			_employeeStrategy = employeeStrategy;
			_employeeRepository = employeeRepository;
		}

		public async Task<decimal> CalculateEmployeeSalaryAsync(EmployeeSalaryRequest employeeSalaryRequest)
		{
			decimal salary = decimal.Zero;
			var result =await _employeeRepository.Get(employeeSalaryRequest.Id);

			if (result == null) throw  new Exception("Employee doesn't exist!");
			EmployeeType type = (EmployeeType)result.TypeId;
			salary = _employeeStrategy.Calculate(type, employeeSalaryRequest.AbsentDays, employeeSalaryRequest.WorkedDays);

			return salary;
		}

	}
}
