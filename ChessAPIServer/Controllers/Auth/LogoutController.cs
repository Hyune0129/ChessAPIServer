using APIServer.DTO;
using APIServer.DTO.Auth;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using ZLogger;

namespace APIServer.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class Logout : ControllerBase
{
    readonly ILogger<Logout> _logger;
    readonly IAuthService _authService;
    public Logout(ILogger<Logout> logger, IAuthService authService)
    {
        _logger = logger;
        _authService = authService;
    }

    [HttpGet]
    public async Task<LogoutResponse> LogoutAccount([FromHeader] HeaderDTO header)
    {
        LogoutResponse response = new();
        response.Result = await _authService.DeleteUserToken(header.Uid);
        _logger.ZLogInformation($"[Logout] UserId:{header.Uid}");
        return response;
    }
}