using Dapper;
using Sprout.Exam.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
	public class EmployeeRepository : IEmployeeRepository
	{

		private readonly IDbConnection _dbConnection;
		public EmployeeRepository(IDbConnection dbconnection)
		{
			_dbConnection = dbconnection;
		}
		private string _tableName = "Employee";
		public async Task<int> AddAsync(CreateEmployee item)
		{
			string query = $"INSERT INTO {_tableName} (FullName, Birthdate, TIN, EmployeeTypeId, IsDeleted) " +
				$"values (@FullName, @Birthdate, @TIN, @EmployeeTypeId, 0); " +
				$"SELECT CAST(SCOPE_IDENTITY() as int)";

			var parameters = new DynamicParameters();
			parameters.Add("FullName",item.FullName, DbType.String);
			parameters.Add("Birthdate", item.Birthdate.ToString("yyyy-MM-dd"), DbType.Date);
			parameters.Add("TIN", item.Tin, DbType.String);
			parameters.Add("EmployeeTypeId", item.EmployeeTypeId, DbType.Int32);

			int employee = await _dbConnection.QuerySingleAsync<int>(query, parameters);
			return employee;
		}

		public async Task<Employee> Get(int id)
		{
			string query = $"select * from {_tableName} where Id = @Id and IsDeleted = 0;";
			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32);

			Employee employee = await _dbConnection.QuerySingleAsync<Employee>(query, parameters);
			return employee;
		}

		public async Task<List<Employee>> GetAll()
		{
			string query = $"select * from {_tableName} where IsDeleted = 0;";

			var result = await _dbConnection.QueryAsync<Employee>(query);
			List<Employee> employees = result.ToList();
			return employees;
		}

		public async Task Remove(int id)
		{
			string query = $"update {_tableName} set IsDeleted = 1 where Id = @Id ;";
			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32);
			await _dbConnection.ExecuteAsync(query, parameters);
		}

		public async Task Update(EditEmployee item)
		{
			string query = $"update {_tableName} " +
				$"set FullName = @FullName, " +
				$"Tin = @Tin, " +
				$"Birthdate = @Birthdate, " +
				$"EmployeeTypeId = @EmployeeTypeId " +
				$"where Id = @Id;";

			var parameters = new DynamicParameters();
			parameters.Add("FullName", item.FullName, DbType.String);
			parameters.Add("Tin", item.Tin, DbType.String);
			parameters.Add("Birthdate", item.Birthdate.ToString("yyyy-MM-dd"), DbType.Date);
			parameters.Add("EmployeeTypeId", item.EmployeeTypeId, DbType.Int32);
			parameters.Add("Id", item.Id, DbType.Int32);
			await _dbConnection.ExecuteAsync(query, parameters);
		}
	}
}
