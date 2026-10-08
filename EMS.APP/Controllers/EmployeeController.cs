using EMS.APP.Data;
using EMS.APP.DTOs;
using EMS.APP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.APP.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController
    : ControllerBase
    {
        private readonly
        ApplicationDbContext _context;

        public EmployeesController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(x => x.EmployeeId == id);

            if (employee == null)
            {
                return NotFound(
                    new { message = "Employee not found" });
            }

            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();

            return Ok(
                new { message = "Employee deleted successfully" });
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check Employee Code Exists
            var employeeCodeExists =
                await _context.Employees
                .AnyAsync(x =>
                    x.EmployeeCode == dto.EmployeeCode);

            if (employeeCodeExists)
            {
                return BadRequest(
                    new
                    {
                        message =
                        "Employee Code already exists"
                    });
            }

            // Check Email Exists
            var emailExists =
                await _context.Employees
                .AnyAsync(x =>
                    x.Email == dto.Email);

            if (emailExists)
            {
                return BadRequest(
                    new
                    {
                        message =
                        "Email already exists"
                    });
            }

            var employee = new Employee
            {
                EmployeeCode = dto.EmployeeCode,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Phone = dto.Phone,
                DepartmentId = dto.DepartmentId,
                Status = dto.Status,
                JoiningDate = dto.JoiningDate,
                CreatedAt = DateTime.UtcNow
            };

            _context.Employees.Add(employee);

            await _context.SaveChangesAsync();

            return Ok(
                new
                {
                    message =
                    "Employee created successfully",
                    employeeId =
                    employee.EmployeeId
                });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, EmployeeDto dto)
        {
            var employee =
                await _context.Employees
                .FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeCode =
                dto.EmployeeCode;

            employee.FirstName =
                dto.FirstName;

            employee.LastName =
                dto.LastName;

            employee.Email =
                dto.Email;

            employee.Phone =
                dto.Phone;

            employee.DepartmentId =
                dto.DepartmentId;

            employee.Status =
                dto.Status;

            employee.JoiningDate =
                dto.JoiningDate;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message =
                "Employee updated successfully"
            });
        }
    }
}
