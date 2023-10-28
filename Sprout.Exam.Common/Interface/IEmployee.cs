using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Interface
{
	public interface IEmployee
	{
		public decimal CalculateSalary(decimal absentDays, decimal workedDays);
	}
}
