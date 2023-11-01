using Sprout.Exam.Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Common.Interface
{
	public interface IEmployeeSalaryCalculator
	{
		public Task<decimal> CalculateEmployeeSalaryAsync(EmployeeSalaryRequest employeeSalaryRequest);
	}
}
