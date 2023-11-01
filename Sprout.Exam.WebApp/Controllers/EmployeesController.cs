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
using Microsoft.Extensions.Logging;
using Sprout.Exam.WebApp.Validator;

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
        private readonly ILogger<EmployeesController> _logger;
        private readonly EmployeeValidator _employeeValidator;
        //constructor 
        public EmployeesController(ILogger<EmployeesController> logger, IEmployeeSalaryCalculator employeeSalaryCalculator, IEmployeeRepository employeeRepository, IMapper mapper, EmployeeValidator employeeValidator)
		{
            _employeeSalaryCalculator = employeeSalaryCalculator;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _logger = logger;
            _employeeValidator = employeeValidator;
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
                var result = await _employeeRepository.GetAllAsync();
                List<EmployeeDto> empDto = _mapper.Map<List<EmployeeDto>>(result);
                _logger.LogInformation("Successfully get all data");
                return Ok(empDto);
            }
			catch (Exception e)
			{
                _logger.LogError(e, "Error in fetching data.");
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
                var result = await _employeeRepository.GetAsync(id);
                EmployeeDto empDto = _mapper.Map<EmployeeDto>(result);
                _logger.LogInformation("Successfully get data");
                return Ok(empDto);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in fetching data.");
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
                Employee item = await _employeeRepository.GetAsync(input.Id);
                _logger.LogInformation("Data not found");
                if (item == null) return NotFound();

                EditEmployee editEmployee = _mapper.Map<EditEmployee>(input);
                await _employeeRepository.UpdateAsync(editEmployee);
                item = await _employeeRepository.GetAsync(input.Id);
                EmployeeDto employeeDto = _mapper.Map<EmployeeDto>(item);
                _logger.LogInformation("Updated successfully.");
                return Ok(employeeDto);
            }
			catch (Exception e)
			{
                _logger.LogError(e, "Error in updating data.");
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
			try
			{
                CreateEmployee createEmployee = _mapper.Map<CreateEmployee>(input);
                var val =  _employeeValidator.ValidateRequest(createEmployee);
				if (val.HasError)
				{
                    _logger.LogError(val.ErrorMessage);
                    return BadRequest(val.ErrorMessage);
                }
               
                var id = await _employeeRepository.AddAsync(createEmployee);
                _logger.LogInformation("Data create.");
                return Created($"/api/employees/{id}", id);
            }
			catch (Exception e)
			{
                _logger.LogError(e, "Error in creating employee.");
                return StatusCode(500, e.Message);
			}
        }

        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
			try
			{
                await _employeeRepository.RemoveAsync(id);
                _logger.LogInformation("Data deleted.");
                return Ok(id);
            }
            catch (Exception e)
			{
                _logger.LogError(e, "Error in deleting employee.");
                return StatusCode(500, e.Message);
			}
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
                var val = _employeeValidator.ValidateRequest(employeeSalaryRequest);
                if (val.HasError)
                {
                    _logger.LogError(val.ErrorMessage);
                    return BadRequest(val.ErrorMessage);
                }
                decimal salary = await _employeeSalaryCalculator.CalculateEmployeeSalaryAsync(employeeSalaryRequest);
                _logger.LogInformation("Successfully calculated.");
                return Ok(salary.ToString("0.00"));
            }
			catch (Exception e)
			{
                _logger.LogError(e, "Error in calculating employee salary.");
                return Ok(e.Message);
            }
        }

    }
}
