using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using core8_angular_mssql.Services;
using AutoMapper;
using core8_angular_mssql.Entities;
using core8_angular_mssql.Models.dto;
using core8_angular_mssql.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace core8_angular_mssql.Controllers.Users
{
    [ApiExplorerSettings(GroupName = "Retrieve User ID")]
    [Authorize]    
    [ApiController]
    [Route("[controller]")]
    public class GetbyidController : ControllerBase
    {

    private IUserService _userService;

    private IMapper _mapper;
    private readonly IConfiguration _configuration;  

    private readonly IWebHostEnvironment _env;

    private readonly ILogger<GetbyidController> _logger;

    public GetbyidController(
        IConfiguration configuration,
        IWebHostEnvironment env,
        IUserService userService,
        IMapper mapper,
        ILogger<GetbyidController> logger
        )
    {
        _configuration = configuration;  
        _userService = userService;
        _mapper = mapper;
        _logger = logger;
        _env = env;        
    }  

        [HttpGet("/api/getbyid/{id}")]
        public IActionResult getByuserid(int id) {
            try {
                var user = _userService.GetById(id);
                var model = _mapper.Map<UserModel>(user);
                return Ok(new {
                    statuscode = 200,
                    message = "User found, please wait.",
                    user = model
                });

            } catch(AppException ex) {
                return NotFound(new {
                    statuscode = 404,
                    message = ex.Message
                });

            }
        }
    }
    
}