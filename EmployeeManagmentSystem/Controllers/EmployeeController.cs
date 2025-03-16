using EmployeeManagmentSystem.Data;
using EmployeeManagmentSystem.Models;
using EmployeeManagmentSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagmentSystem.Controllers
{
    [Route("api/employees")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_employeeService.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null) return NotFound();

            return Ok(employee);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            var createdEmployee = _employeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(GetById), new { id = createdEmployee.Id }, createdEmployee);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee employee)
        {
            var updatedEmployee = _employeeService.UpdateEmployee(id, employee);
            if (updatedEmployee == null)
                return NotFound();

            return Ok(updatedEmployee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var success = _employeeService.DeleteEmployee(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
