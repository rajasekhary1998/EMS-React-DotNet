using EMS.APP.Data;
using EMS.APP.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.APP.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController
     : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DashboardController(
            ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetDashboard()
        {
            var result = new DashboardDto
            {
                TotalEmployees =
                    await _context.Employees.CountAsync(),

                ActiveEmployees =
                    await _context.Employees
                        .CountAsync(x => x.Status == "ACTIVE"),

                TotalDepartments =
                    await _context.Departments.CountAsync(),

                InactiveEmployees =
                    await _context.Employees
                        .CountAsync(x => x.Status == "INACTIVE")
            };

            return Ok(result);
        }
    }
}
