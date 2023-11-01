using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
	public interface IEmployeeRepository
	{
		Task<List<Employee>> GetAllAsync();
		Task<Employee> GetAsync(int id);
		Task<int> AddAsync(CreateEmployee item);
		Task RemoveAsync(int id);
		Task UpdateAsync(EditEmployee item);
	}
}
