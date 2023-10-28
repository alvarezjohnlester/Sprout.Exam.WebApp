using NUnit.Framework;
using Sprout.Exam.DataAccess.Repository;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sprout.Exam.NunitTest
{
	public class Tests
	{

		private string _connectionString = "";
		private EmployeeRepository _employeeRepository = null;
		[SetUp]
		public void Setup()
		{
			_connectionString = "Server=localhost;Database=SproutExamDb;User Id=localdb;Password=localdbadmin;";
			IDbConnection dbconnection = new SqlConnection(_connectionString);
			_employeeRepository = new EmployeeRepository(dbconnection);
		}

		[Test]
		public void TestGetAll()
		{
			try
			{
				var result = _employeeRepository.GetAll().Result;
			}
			catch
			{
				Assert.Fail();
			}
			Assert.Pass();
		}
	}
}