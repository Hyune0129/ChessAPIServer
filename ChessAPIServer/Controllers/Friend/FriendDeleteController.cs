
using APIServer.DTO;
using APIServer.DTO.Friend;
using APIServer.Services.Friend;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Friend;

[ApiController]
[Route("[controller]")]
public class FriendDelete : ControllerBase
{
    readonly ILogger<FriendDelete> _logger;
    readonly IFriendService _friendService;

    public FriendDelete(ILogger<FriendDelete> logger, IFriendService friendService)
    {
        _logger = logger;
        _friendService = friendService;
    }

    [HttpPost]
    public async Task<FriendDeleteResponse> DeleteFriend([FromHeader] HeaderDTO header, FriendDeleteRequest request)
    {
        FriendDeleteResponse response = new();
        response.Result = await _friendService.DeleteFriend(header.Uid, request.friendUid);
        _logger.ZLogInformation($"[FriendDeleteController.DeleteFriend] uid : {header.Uid}, friendUid : {request.friendUid}");
        return response;
    }
}