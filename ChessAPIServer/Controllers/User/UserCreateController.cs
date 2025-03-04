using APIServer.DTO.User;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.User;

[ApiController]
[Route("[controller]")]
public class UserCreate
{
    readonly ILogger<UserCreate> _logger;
    readonly IUserService _userService;

    public UserCreate(ILogger<UserCreate> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task<UserCreateResponse> CreateUser(UserCreateRequest request)
    {
        UserCreateResponse response = new();
        response.Result = await _userService.CreateUser(request.Uid, request.Nickname);
        _logger.ZLogInformation($"[CreateUser] uid : {request.Uid}, Nickname : {request.Nickname}");
        return response;
    }
}