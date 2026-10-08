using EMS.APP.Data;
using EMS.APP.DTOs;
using EMS.APP.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace EMS.APP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DesignationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await (
                from d in _context.Designations
                join dept in _context.Departments
                    on d.DepartmentId equals dept.DepartmentId
                select new DesignationDto
                {
                    DesignationId = d.DesignationId,
                    DesignationName = d.DesignationName,
                    DepartmentId = d.DepartmentId,
                    DepartmentName = dept.DepartmentName,
                    CreatedAt = d.CreatedAt
                })
                .ToListAsync();

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDesignationDto dto)
        {
            var exists = await _context.Designations
                .AnyAsync(x => x.DesignationName.ToLower() == dto.DesignationName.ToLower());

            if (exists)
            {
                return BadRequest(new
                {
                    message = "Designation already exists."
                });
            }

            var designation = new Designation
            {
                DesignationName = dto.DesignationName,
                DepartmentId = dto.DepartmentId
            };

            _context.Designations.Add(designation);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Designation created successfully.",
                data = designation
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CreateDesignationDto dto)
        {
            var designation =
                await _context.Designations.FindAsync(id);

            if (designation == null)
                return NotFound();

            designation.DesignationName =
                dto.DesignationName;

            designation.DepartmentId =
                dto.DepartmentId;

            await _context.SaveChangesAsync();

            return Ok(designation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            var designation =
                await _context.Designations.FindAsync(id);

            if (designation == null)
                return NotFound();

            _context.Designations.Remove(designation);

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
