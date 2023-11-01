using System;
using System.Collections.Generic;
using System.Text;

namespace Sprout.Exam.DataAccess
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Tin { get; set; }
        public int EmployeeTypeId { get; set; }
    }
}
