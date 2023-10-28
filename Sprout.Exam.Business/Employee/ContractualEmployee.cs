using Sprout.Exam.Business.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Employee
{
	public class ContractualEmployee : IEmployee
	{
		public decimal CalculateSalary(decimal absentDays, decimal workedDays)
		{
			return 2500;
		}
	}
}
