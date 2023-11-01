using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Common.Model
{
	public class EmployeeSalaryRequest
	{
		public int Id { get; set; }
		public decimal  AbsentDays { get; set; }
		public decimal  WorkedDays { get; set; }
		public decimal  Salary { get; set; }

	}
}
