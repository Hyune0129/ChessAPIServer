using APIServer.DTO;
using APIServer.DTO.Friend;
using APIServer.Services.Friend;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Friend;

[ApiController]
[Route("[controller]")]
public class FriendList : ControllerBase
{
    readonly ILogger<FriendList> _logger;
    readonly IFriendService _friendService;


    public FriendList(ILogger<FriendList> logger, IFriendService friendService)
    {
        _logger = logger;
        _friendService = friendService;
    }

    [HttpPost]
    public async Task<FriendListResponse> ListFriend([FromHeader] HeaderDTO header)
    {
        FriendListResponse response = new();
        (response.Result, response.FriendList) = await _friendService.GetFriendList(header.Uid);
        _logger.ZLogInformation($"[ListFriend] Uid : {header.Uid}");
        return response;
    }
}