using EMS.APP.Data;
using EMS.APP.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequestDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
        if (user == null)
        {
            return Unauthorized(new
            {
                message = "Invalid Email"
            });
        }
        if (user.Password != request.Password)
        {
            return Unauthorized(new
            {
                message = "Invalid Password"
            });
        }
        return Ok(new
        {
            user.UserId,
            user.FullName,
            user.Email,
            user.Role
        });
    }
}