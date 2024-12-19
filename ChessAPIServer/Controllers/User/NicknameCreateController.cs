using APIServer.DTO.User;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.User;

[ApiController]
[Route("[controller]")]
public class NicknameCreate
{
    readonly ILogger<NicknameCreate> _logger;
    readonly IUserService _userService;

    public NicknameCreate(ILogger<NicknameCreate> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task<NicknameCreateResponse> CreateNickname(NicknameCreateRequest request)
    {
        NicknameCreateResponse response = new();
        response.Result = await _userService.CreateNickname(request.Uid, request.Nickname);
        _logger.ZLogInformation($"[CreateNickname] uid : {request.Uid}, Nickname : {request.Nickname}");
        return response;
    }
}