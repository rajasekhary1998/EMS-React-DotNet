// Controllers/DepartmentsController.cs

using EMS.APP.Data;
using EMS.APP.DTOs;
using EMS.APP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.APP.Controllers
{
    [ApiController]
    [Route("api/departments")]
    public class DepartmentsController
        : ControllerBase
    {
        private readonly ApplicationDbContext
            _context;

        public DepartmentsController(
            ApplicationDbContext context)
        {
            _context = context;
        }

        // GET

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments =
                await _context.Departments.ToListAsync();

            return Ok(departments);
        }
        // POST
        [HttpPost]
        public async Task<IActionResult> CreateDepartment(DepartmentDto dto)
        {
            var departmentName = dto.DepartmentName?.Trim();

            if (string.IsNullOrWhiteSpace(departmentName))
            {
                return BadRequest(new
                {
                    message = "Department name is required"
                });
            }

            var exists = await _context.Departments
                .AnyAsync(d =>
                    d.DepartmentName.ToLower().Trim() ==
                    departmentName.ToLower()
                );

            if (exists)
            {
                return BadRequest(new
                {
                    message = "Department already exists"
                });
            }

            var department = new Departments
            {
                DepartmentName = departmentName,
                CreatedAt = DateTime.UtcNow   // better than frontend date
            };

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Department added successfully"
            });
        }

        // PUT

        [HttpPut("{id}")]
        public async Task<IActionResult>
            UpdateDepartment(
                int id,
                DepartmentDto dto)
        {
            var department =
                await _context.Departments
                .FindAsync(id);

            if (department == null)
            {
                return NotFound(
                    new
                    {
                        message =
                        "Department not found"
                    });
            }

            department.DepartmentName =
                dto.DepartmentName;

            await _context.SaveChangesAsync();

            return Ok(
                new
                {
                    message =
                    "Department updated successfully"
                });
        }

        // DELETE

        [HttpDelete("{id}")]
        public async Task<IActionResult>
            DeleteDepartment(int id)
        {
            var department =
                await _context.Departments
                .FindAsync(id);

            if (department == null)
            {
                return NotFound(
                    new
                    {
                        message =
                        "Department not found"
                    });
            }

            _context.Departments
                .Remove(department);

            await _context.SaveChangesAsync();

            return Ok(
                new
                {
                    message =
                    "Department deleted successfully"
                });
        }
    }
}