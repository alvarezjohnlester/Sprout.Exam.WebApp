using Sprout.Exam.Common.Model;
using Sprout.Exam.DataAccess;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Validator
{
	public class EmployeeValidator
	{
		public EmployeeValidator()
		{

		}

		public ValidatorResponse ValidateRequest(CreateEmployee createEmployee)
		{
			ValidatorResponse response = new ValidatorResponse();
			try
			{

				if (string.IsNullOrEmpty(createEmployee.FullName))
				{
					response.HasError = true;
					response.ErrorMessage = "Fullname can't be null or empty!";
					return response;
				}
				if (!Regex.IsMatch("^[A-Z][a-z]*(\\s[A-Z][a-z]*)+$", createEmployee.FullName))
				{
					response.HasError = true;
					response.ErrorMessage = "Full name contains invalid inputs.";
					return response;
				}
				if (createEmployee.EmployeeTypeId <= 0)
				{
					response.HasError = true;
					response.ErrorMessage = "Invalid employee type";
					return response;
				}
				response.HasError = false;
			}
			catch (Exception e)
			{
				response.HasError = true;
				response.ErrorMessage = e.Message;
			}
			return response;
		}
		public ValidatorResponse ValidateRequest(EmployeeSalaryRequest employeeSalaryRequest)
		{
			ValidatorResponse response = new ValidatorResponse();
			try
			{

				if (employeeSalaryRequest.AbsentDays < 0 )
				{
					response.HasError = true;
					response.ErrorMessage = "Absent Days must be greater than or equals to 0!";
					return response;
				}
				if (employeeSalaryRequest.WorkedDays < 0)
				{
					response.HasError = true;
					response.ErrorMessage = "Worked Days must be greater than or equals to 0!";
					return response;
				}
				if (employeeSalaryRequest.Id <= 0)
				{
					response.HasError = true;
					response.ErrorMessage = "Invalid ID!";
					return response;
				}
				if (employeeSalaryRequest.Salary < 0)
				{
					response.HasError = true;
					response.ErrorMessage = "Salary must be greater than or equals to 0!";
					return response;
				}
				response.HasError = false;
			}
			catch (Exception e)
			{
				response.HasError = true;
				response.ErrorMessage = e.Message;
			}
			return response;
		}

	}
}
