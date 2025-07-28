using System.Diagnostics;
using core8_angular_mssql.Models.dto;
using core8_angular_mssql.Services;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using core8_angular_mssql.Entities;
using core8_angular_mssql.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace core8_angular_mssql.Controllers.Auth
{
    
[ApiExplorerSettings(GroupName = "Sign-up or Account Registration")]
[ApiController]
[Route("[controller]")]
public class RegisterController : ControllerBase
{
    private IAuthService _authService;
    private IMapper _mapper;

    private readonly IWebHostEnvironment _env;

    private readonly ILogger<RegisterController> _logger;

    public RegisterController(
        IWebHostEnvironment env,
        IAuthService userService,
        IMapper mapper,
        ILogger<RegisterController> logger
        )
    {   
        _authService = userService;
        _mapper = mapper;
        _logger = logger;
        _env = env;
    }  

    [HttpPost("/signup")]
    public IActionResult signup(UserRegister model) {
        DateTime now = DateTime.Now;
        // string formattedDate = now.ToString("yyyy-MM-dd HH:mm:ss");
        // Console.WriteLine(formattedDate);

        var user = _mapper.Map<User>(model);
            try
            {
                user.LastName = model.Lastname;
                user.FirstName = model.Firstname;
                user.Email = model.Email;
                user.Mobile = model.Mobile;
                user.UserName = model.Username;
                user.CreatedAt = now;
                _authService.SignupUser(user, model.Password);
                return Ok(new {statuscode = 200, message = "You have registered successfully."});
            }
            catch (AppException ex)
            {
                return Ok(new { statuscode = 404, message = ex.Message });
            }
    }
}
    
}