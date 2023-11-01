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
		private Dictionary<EmployeeType, decimal> _employeeSalary = new Dictionary<EmployeeType, decimal> { 
			{EmployeeType.Contractual, 500.00m},
			{EmployeeType.Regular, 20000.00m}
		};
		public EmployeeSalaryCalculator(EmployeeStrategy employeeStrategy, IEmployeeRepository employeeRepository)
		{
			_employeeStrategy = employeeStrategy;
			_employeeRepository = employeeRepository;
		}

		public async Task<decimal> CalculateEmployeeSalaryAsync(EmployeeSalaryRequest employeeSalaryRequest)
		{
			decimal Monthlysalary = decimal.Zero;
			//getting employee
			var result = await _employeeRepository.GetAsync(employeeSalaryRequest.Id);
			if (result == null) throw  new Exception("Employee doesn't exist!");
			
			EmployeeType type = (EmployeeType)result.EmployeeTypeId;
			//for improvements
			// put salary details in table 
			// for now we use static salary
			employeeSalaryRequest.Salary = GetEmployeeSalary(type);
			Monthlysalary = _employeeStrategy.Calculate(type, employeeSalaryRequest);

			return Monthlysalary;
		}

		private decimal GetEmployeeSalary(EmployeeType employeeType)
		{
			decimal salary = decimal.Zero;
			_employeeSalary.TryGetValue(employeeType, out salary);
			return salary;
		}
	}
}
