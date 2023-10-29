using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.WebApp.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Business.Core;
using Sprout.Exam.Common.Model;
using Sprout.Exam.Common.Interface;
using Sprout.Exam.DataAccess.Repository;
using Sprout.Exam.DataAccess;
using AutoMapper;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeSalaryCalculator _employeeSalaryCalculator = null;
        private readonly IEmployeeRepository _employeeRepository = null;
        private readonly IMapper _mapper;
        //constructor 
        public EmployeesController(IEmployeeSalaryCalculator employeeSalaryCalculator, IEmployeeRepository employeeRepository, IMapper mapper)
		{
            _employeeSalaryCalculator = employeeSalaryCalculator;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
			try
			{
                var result = await _employeeRepository.GetAll();
                List<EmployeeDto> empDto = _mapper.Map<List<EmployeeDto>>(result);
                return Ok(empDto);
            }
			catch (Exception e)
			{
                return StatusCode(500,e.Message);
			}
            
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _employeeRepository.Get(id);
                EmployeeDto empDto = _mapper.Map<EmployeeDto>(result);
                return Ok(empDto);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }

           
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
			try
			{
                Employee item = await _employeeRepository.Get(input.Id);
                if (item == null) return NotFound();
                EditEmployee editEmployee = _mapper.Map<EditEmployee>(input);
                await _employeeRepository.Update(editEmployee);
                item = await _employeeRepository.Get(input.Id);
                EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(item);
                return Ok(employeeDto);
            }
			catch (Exception e)
			{
                return StatusCode(500, e.Message);
            }
            
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {

           var id = await Task.FromResult(StaticEmployees.ResultList.Max(m => m.Id) + 1);

            //StaticEmployees.ResultList.Add(new EmployeeDto
            //{
            //    Birthdate = input.Birthdate.ToString("yyyy-MM-dd"),
            //    FullName = input.FullName,
            //    Id = id,
            //    Tin = input.Tin,
            //    TypeId = input.TypeId
            //});

            return Created($"/api/employees/{id}", id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeRepository.Remove(id);
            return Ok(id);
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate([FromBody] EmployeeSalaryRequest employeeSalaryRequest)
        {
			try
			{
                //EmployeeSalaryRequest request = new EmployeeSalaryRequest();
                //request.EmployeeType = type;
                //request.AbsentDays = absentDays;
                //request.WorkedDays = workedDays;
                decimal salary = await _employeeSalaryCalculator.CalculateEmployeeSalaryAsync(employeeSalaryRequest);
                return Ok(salary);
            }
			catch (Exception e)
			{
                return Ok(e.Message);
            }
        }

    }
}
