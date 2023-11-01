using Sprout.Exam.Business.Interface;
using Sprout.Exam.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employee
{
	public class RegularEmployee : IEmployee
	{
		public decimal CalculateSalary(EmployeeSalaryRequest employeeSalaryRequest)
		{
			decimal salary = decimal.Zero;
			decimal monthly = employeeSalaryRequest.Salary;
			decimal deduction = decimal.Round((monthly /22) * employeeSalaryRequest.AbsentDays, 2, MidpointRounding.AwayFromZero);
			decimal tax = 0.12m;
			decimal taxDeduction = decimal.Round(monthly * tax, 2, MidpointRounding.AwayFromZero);

			salary = decimal.Round((monthly - deduction - taxDeduction), 
						2, MidpointRounding.AwayFromZero);

			return salary;
		}
	}
}