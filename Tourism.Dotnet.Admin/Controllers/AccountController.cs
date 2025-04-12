using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tourism.Dotnet.Admin.DAL.Context;
using Tourism.Dotnet.Admin.DAL.Entities;
using Tourism.Dotnet.Admin.DTO;
using Tourism.Dotnet.Admin.Services;

namespace Tourism.Dotnet.Admin.Controllers;

[ApiController]
[Route("/api/account")]
public class AccountController(UserService userService) : ControllerBase
{
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromQuery] UserRegisterDto userRegisterDto)
    {
        await userService.Register(userRegisterDto.UserName, userRegisterDto.Email, userRegisterDto.Password);
        return Ok();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromQuery] UserLoginDto userLoginDto)
    {
        var token = await userService.Login(userLoginDto.Email, userLoginDto.Password);
        HttpContext.Response.Cookies.Append("token", token, new CookieOptions() { SameSite = SameSiteMode.Lax });
        return Ok(token);
    }
    [Authorize]
    [HttpGet("All")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await userService.GetAllUsers());
    }

    [HttpGet("Validate")]
    public async Task<IActionResult> Validate([FromQuery] string email)
    {
        var user = await userService.GetByEmail(email);
        var token = HttpContext.Request.Cookies["token"];
        if(token == null)
            return Unauthorized();

        var userId = userService.Validate(token);
        if(user.Id.ToString() != userId)
            return Unauthorized();
        return Ok(userId);
    }
}