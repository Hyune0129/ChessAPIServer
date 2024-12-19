using APIServer.DTO;
using APIServer.DTO.Friend;
using APIServer.Services.Friend;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Friend;

[ApiController]
[Route("[controller]")]
public class FriendDenyController : ControllerBase
{

    readonly ILogger<FriendDenyController> _logger;
    readonly IFriendService _friendService;

    public FriendDenyController(ILogger<FriendDenyController> logger, IFriendService friendService)
    {
        _logger = logger;
        _friendService = friendService;
    }

    [HttpPost]
    public async Task<FriendDenyResponse> DenyFriend([FromHeader] HeaderDTO header, FriendDenyRequest request)
    {
        FriendDenyResponse response = new();
        response.Result = await _friendService.DenyFriend(header.Uid, request.FriendId);
        _logger.ZLogInformation($"[DenyFriend] Uid:{header.Uid}, FriendId : {request.FriendId}");
        return response;
    }

}