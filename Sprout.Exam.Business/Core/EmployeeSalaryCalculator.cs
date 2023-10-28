using Sprout.Exam.Business.Factory;
using Sprout.Exam.Business.Interface;
using Sprout.Exam.Business.Strategy;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Interface;
using Sprout.Exam.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Core
{
	public class EmployeeSalaryCalculator : IEmployeeSalaryCalculator
	{
		private EmployeeStrategy _employeeStrategy;
		public EmployeeSalaryCalculator(EmployeeStrategy employeeStrategy)
		{
			_employeeStrategy = employeeStrategy;
		}

		public decimal CalculateEmployeeSalary(EmployeeSalaryRequest employeeSalaryRequest)
		{
			decimal salary = decimal.Zero;

			salary = _employeeStrategy.Calculate(employeeSalaryRequest.EmployeeType, employeeSalaryRequest.AbsentDays, employeeSalaryRequest.WorkedDays);

			return salary;
		}

	}
}
