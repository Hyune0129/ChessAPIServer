using APIServer.DTO.Auth;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Auth;

[ApiController]
[Route("[controller]")]
public class Login : ControllerBase
{
    readonly ILogger<Login> _logger;
    readonly IAuthService _authService;
    readonly IGameService _gameService;

    readonly IDataLoadService _dataLoadService;

    public Login(ILogger<Login> logger, IAuthService authService, IGameService gameService, IDataLoadService dataLoadService)
    {
        _logger = logger;
        _authService = authService;
        _gameService = gameService;
        _dataLoadService = dataLoadService;
    }


    ///  <summary>
    /// login api.
    /// uid and token verify
    /// uid value == pid value
    /// get userdata from chess server
    /// </summary>

    [HttpPost]
    public async Task<LoginResponse> LoginAndLoadData(LoginRequest request)
    {
        LoginResponse response = new();

        // verify token from login server
        // lastlogin time update

        response.Result = await _authService.VerifyTokenToLoginServer(request.UserId, request.AccountToken);
        if (response.Result != ErrorCode.None)
        {
            // if new user response ErrorCode 2005
            return response;
        }

        // user data load
        (response.Result, response.userData) = await _dataLoadService.LoadUserData(request.UserId);

        if (response.Result != ErrorCode.None)
        {
            return response;
        }

        // update last login time
        response.Result = await _authService.UpdateLastLoginTime(request.UserId);
        if (response.Result != ErrorCode.None)
        {
            return response;
        }

        _logger.ZLogInformation($"[Login] PlayerId :{request.UserId}");
        return response;
    }
}