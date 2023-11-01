using Sprout.Exam.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Interface
{
	public interface IEmployee
	{
		public decimal CalculateSalary(EmployeeSalaryRequest employeeSalaryRequest);
	}
}
