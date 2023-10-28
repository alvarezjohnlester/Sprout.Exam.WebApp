﻿using Sprout.Exam.Business.Factory;
using Sprout.Exam.Business.Interface;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Strategy
{
	public class EmployeeStrategy
	{
		private EmployeeFactory _employeeFactory = null;
		public EmployeeStrategy(EmployeeFactory employeeFactory)
		{
			_employeeFactory = employeeFactory;
		}

		public decimal Calculate(EmployeeType employeeType, decimal absentDays, decimal workedDays)
		{
			IEmployee employee = _employeeFactory.GetInstanceType(employeeType);
			if (employee == null)
			{
				throw new Exception("Employee doesn't exist!");
			}

			decimal salary = employee.CalculateSalary(absentDays, workedDays);

			return salary;
		}
	}
}
