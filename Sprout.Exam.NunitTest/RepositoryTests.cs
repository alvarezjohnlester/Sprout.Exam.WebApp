using NUnit.Framework;
using Sprout.Exam.Business.Core;
using Sprout.Exam.Business.Employee;
using Sprout.Exam.Common.Interface;
using Sprout.Exam.Common.Model;
using Sprout.Exam.DataAccess;
using Sprout.Exam.DataAccess.Repository;
using Sprout.Exam.WebApp.DataTransferObjects;
using Sprout.Exam.WebApp.Validator;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sprout.Exam.NunitTest
{
	public class Tests
	{

		private string _connectionString = "";
		private EmployeeRepository _employeeRepository = null;
		private int _employeeId = 5;
		private EmployeeValidator _employeeValidator;
		private IEmployeeSalaryCalculator _employeeSalaryCalculator = null;
		[SetUp]
		public void Setup()
		{
			_connectionString = "Server=localhost;Database=SproutExamDb;User Id=localdb;Password=localdbadmin;";
			IDbConnection dbconnection = new SqlConnection(_connectionString);
			_employeeRepository = new EmployeeRepository(dbconnection);
			_employeeValidator = new EmployeeValidator();

			Business.Factory.EmployeeFactory employeeFactory = new Business.Factory.EmployeeFactory();
			employeeFactory.AddEmployee(Common.Enums.EmployeeType.Contractual, new ContractualEmployee());
			employeeFactory.AddEmployee(Common.Enums.EmployeeType.Regular, new RegularEmployee());

			_employeeSalaryCalculator = new EmployeeSalaryCalculator(
				new Business.Strategy.EmployeeStrategy(employeeFactory),
				_employeeRepository) ;
		}
		[Test]
		public void TestCreateEmployee()
		{
			try
			{
				CreateEmployee createEmployee = new CreateEmployee();
				createEmployee.Birthdate = new DateTime(1993, 05, 28);
				createEmployee.FullName = "Test FullName";
				createEmployee.Tin = "1234";
				createEmployee.EmployeeTypeId = 1;


				var result = _employeeRepository.AddAsync(createEmployee).Result;
				_employeeId = result;
				Assert.Positive(_employeeId);
			}
			catch
			{
				Assert.Fail();
			}
			
		}
		[Test]
		public void TestDeleteEmployee()
		{
			try
			{
				CreateEmployee createEmployee = new CreateEmployee();
				createEmployee.Birthdate = new DateTime(1993, 05, 28);
				createEmployee.FullName = "Test FullName";
				createEmployee.Tin = "1234";
				createEmployee.EmployeeTypeId = 1;


				var result = _employeeRepository.AddAsync(createEmployee).Result;
				_employeeRepository.RemoveAsync(result).Wait();
			}
			catch
			{
				Assert.Fail();
			}
			Assert.Pass();
		}
		[Test]
		public void TestUpdateEmployee()
		{
			try
			{
				EditEmployee editEmployee = new EditEmployee();
				editEmployee.Birthdate = new DateTime(1993, 05, 28);
				editEmployee.FullName = "Test FullName";
				editEmployee.Tin = "1234";
				editEmployee.EmployeeTypeId = 1;
				editEmployee.Id = 5;

				_employeeRepository.UpdateAsync(editEmployee).Wait();
			}
			catch
			{
				Assert.Fail();
			}
			Assert.Pass();
		}
		[Test]
		public void TestGetAll()
		{
			try
			{
				var result = _employeeRepository.GetAllAsync().Result;
			}
			catch
			{
				Assert.Fail();
			}
			Assert.Pass();
		}
		[Test]
		public void TestGetEmployeePass()
		{
			try
			{
				var result = _employeeRepository.GetAsync(_employeeId).Result;
			}
			catch
			{
				Assert.Fail();
			}
			Assert.Pass();
		}
		[Test]
		public void TestValidatorCreateEmployeePass()
		{
			try
			{
				CreateEmployee createEmployee = new CreateEmployee();
				createEmployee.FullName = "test";

				var val = _employeeValidator.ValidateRequest(createEmployee);
				Assert.IsFalse(val.HasError);
			}
			catch
			{
				Assert.Fail();
			}
		}
		[Test]
		public void TestCalculateContractualEmployeeSalaryPass()
		{
			try
			{
				EmployeeSalaryRequest employeeSalaryRequest = new EmployeeSalaryRequest();
				employeeSalaryRequest.AbsentDays = 0;
				employeeSalaryRequest.WorkedDays = 15.5m;
				employeeSalaryRequest.Salary = 500;
				employeeSalaryRequest.Id = 3;

				decimal salary =  _employeeSalaryCalculator.CalculateEmployeeSalaryAsync(employeeSalaryRequest).Result;

				Assert.AreEqual(7750.00m, salary);
			}
			catch
			{
				Assert.Fail();
			}
		}
		[Test]
		public void TestCalculateRegularEmployeeSalaryPass()
		{
			try
			{
				EmployeeSalaryRequest employeeSalaryRequest = new EmployeeSalaryRequest();
				employeeSalaryRequest.AbsentDays = 1;
				employeeSalaryRequest.WorkedDays = 0;
				employeeSalaryRequest.Salary = 20000;
				employeeSalaryRequest.Id = 4;

				decimal salary = _employeeSalaryCalculator.CalculateEmployeeSalaryAsync(employeeSalaryRequest).Result;

				Assert.AreEqual(16690.91m, salary);
			}
			catch
			{
				Assert.Fail();
			}
		}
		[Test]
		public void TestValidatorCreateEmployeeFail()
		{
			try
			{
				CreateEmployee createEmployee = new CreateEmployee();
				createEmployee.FullName = "@";

				var val = _employeeValidator.ValidateRequest(createEmployee);
				Assert.IsTrue(val.HasError);
			}
			catch
			{
				Assert.Fail();
			}
		}
		[Test]
		public void TestValidatorCalculateEmployeePass()
		{
			try
			{
				EmployeeSalaryRequest employeeSalaryRequest = new EmployeeSalaryRequest();
				employeeSalaryRequest.AbsentDays = 0;
				employeeSalaryRequest.WorkedDays = 0;
				employeeSalaryRequest.Salary = 0;

				var val = _employeeValidator.ValidateRequest(employeeSalaryRequest);
				Assert.IsFalse(val.HasError);
			}
			catch
			{
				Assert.Fail();
			}
		}
	}
}