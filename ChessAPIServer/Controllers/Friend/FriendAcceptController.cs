using APIServer.DTO;
using APIServer.DTO.Friend;
using APIServer.Services.Friend;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Friend;

[ApiController]
[Route("[controller]")]
public class FriendAccept : ControllerBase
{
    readonly ILogger<FriendAccept> _logger;
    readonly IFriendService _friendService;

    public FriendAccept(ILogger<FriendAccept> logger, IFriendService friendService)
    {
        _logger = logger;
        _friendService = friendService;
    }

    [HttpPost]
    public async Task<FriendAcceptResponse> AcceptFriend([FromHeader] HeaderDTO header, FriendAcceptRequest request)
    {
        FriendAcceptResponse response = new();
        response.Result = await _friendService.AcceptFriend(header.Uid, request.FriendId);
        _logger.ZLogInformation($"[AcceptFriend] uid : {header.Uid}, FriendUid : {request.FriendId}");
        return response;
    }
}