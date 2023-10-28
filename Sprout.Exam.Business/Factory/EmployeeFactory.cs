using Sprout.Exam.Business.Employee;
using Sprout.Exam.Business.Interface;
using Sprout.Exam.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.Business.Factory
{
	public class EmployeeFactory
	{
		private Dictionary<EmployeeType, IEmployee> _employeeTypes;

		public EmployeeFactory()
		{
			_employeeTypes = new Dictionary<EmployeeType, IEmployee>();
		}
		public void AddEmployee(EmployeeType employeeType, IEmployee employee)
		{
			if(GetInstanceType(employeeType) == null)
			{
				_employeeTypes.Add(EmployeeType.Regular, employee);
			}
			else
			{
				throw new Exception("Employee already exist!");
			}
		}
		public IEmployee GetInstanceType(EmployeeType employeeType)
		{
			IEmployee employee = null;
			_employeeTypes.TryGetValue(employeeType, out employee);
			return employee;
		}
	}
}
