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
		public Dictionary<EmployeeType, IEmployee> _employeeTypes = new Dictionary<EmployeeType, IEmployee>();

		public EmployeeFactory()
		{
			_employeeTypes.Add(EmployeeType.Regular,new RegularEmployee());
			_employeeTypes.Add(EmployeeType.Contractual, new ContractualEmployee());
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
