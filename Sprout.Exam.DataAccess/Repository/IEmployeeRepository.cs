using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.DataAccess.Repository
{
	public interface IEmployeeRepository
	{
		Task<List<Employee>> GetAll();
		Task<Employee> Get(int id);
		Task<Employee> AddAsync(CreateEmployee item);
		Task Remove(int id);
		Task Update(EditEmployee item);
	}
}
