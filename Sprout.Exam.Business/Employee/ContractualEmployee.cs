using Sprout.Exam.Business.Interface;
using Sprout.Exam.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employee
{
	public class ContractualEmployee : IEmployee
	{
		public decimal CalculateSalary(EmployeeSalaryRequest employeeSalaryRequest)
		{
			decimal salary = decimal.Zero;

			salary = decimal.Round((employeeSalaryRequest.Salary * employeeSalaryRequest.WorkedDays), 2, MidpointRounding.AwayFromZero);

			return salary;
		}
	}
}
