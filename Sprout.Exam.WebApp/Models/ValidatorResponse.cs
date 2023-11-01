using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Models
{
	public class ValidatorResponse
	{
		public bool HasError { get; set; }
		public string ErrorMessage { get; set; }
	}
}
