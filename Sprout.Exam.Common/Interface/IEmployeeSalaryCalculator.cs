using Sprout.Exam.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.Interface
{
	public interface IEmployeeSalaryCalculator
	{
		public decimal CalculateEmployeeSalary(EmployeeSalaryRequest employeeSalaryRequest);
	}
}
