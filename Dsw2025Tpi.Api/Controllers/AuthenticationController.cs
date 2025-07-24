using Dsw2025Tpi.Application.Dtos;
using Dsw2025Tpi.Application.Interfaces;
using Dsw2025Tpi.Domain.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Dsw2025Tpi.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<IdentityUserCustomer> _userManager;
    private readonly SignInManager<IdentityUserCustomer> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly ICustomerManagmentsService _customerManagementsService;

    public AuthenticationController(IJwtTokenService jwtTokenService, SignInManager<IdentityUserCustomer> signInManager, UserManager<IdentityUserCustomer> userManager, ICustomerManagmentsService customerManagementsService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
        _customerManagementsService = customerManagementsService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        if (loginModel == null || string.IsNullOrEmpty(loginModel.UserName) || string.IsNullOrEmpty(loginModel.Password))
        {
            return BadRequest("Invalid login request.");
        }
        var user = await _userManager.FindByNameAsync(loginModel.UserName);
        if (user == null)
        {
            return Unauthorized("Invalid username or password.");
        }
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
        if (!result.Succeeded)
        {
            return Unauthorized("Invalid username or password.");
        }
        var token = _jwtTokenService.GenerateToken(user.UserName, "");
        return Ok(new { Token = token });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        if (registerModel == null || string.IsNullOrEmpty(registerModel.UserName) || string.IsNullOrEmpty(registerModel.Password))
        {
            return BadRequest("Invalid registration request.");
        }
        var customer = await _customerManagementsService.CreateCustomerAsync(registerModel.Email, registerModel.Name, registerModel.PhoneNumber);
       
        var user = new IdentityUserCustomer
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            CustomerId = customer.Id
        };
        var result = await _userManager.CreateAsync(user, registerModel.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok("User registered successfully.");
    }

}
