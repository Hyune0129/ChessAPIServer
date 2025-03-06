using APIServer.DTO;
using APIServer.DTO.User;
using APIServer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.User;

[ApiController]
[Route("[controller]")]
public class NicknameChange
{
    readonly ILogger<NicknameChange> _logger;
    readonly IUserService _userService;

    public NicknameChange(ILogger<NicknameChange> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost]
    public async Task<NicknameChangeResponse> ChangeNickname([FromHeader] HeaderDTO header, NicknameChangeRequest request)
    {
        NicknameChangeResponse response = new();
        response.Result = await _userService.ChangeNickname(header.Uid, request.Nickname);
        _logger.ZLogInformation($"[NicknameChange] uid : {header.Uid}, nickname: {request.Nickname}");
        return response;
    }
}