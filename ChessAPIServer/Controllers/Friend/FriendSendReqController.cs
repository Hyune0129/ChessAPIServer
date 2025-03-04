using APIServer.DTO;
using APIServer.DTO.Friend;
using APIServer.Services.Friend;
using Microsoft.AspNetCore.Mvc;
using ZLogger;

namespace APIServer.Controllers.Friend;

[ApiController]
[Route("[controller]")]
public class FriendSendReq : ControllerBase
{
    readonly ILogger<FriendSendReq> _logger;
    readonly IFriendService _friendService;

    public FriendSendReq(ILogger<FriendSendReq> logger, IFriendService friendService)
    {
        _logger = logger;
        _friendService = friendService;
    }

    [HttpPost]
    public async Task<FriendSendReqResponse> SendFriendReq([FromHeader] HeaderDTO header, FriendSendReqRequest request)
    {
        FriendSendReqResponse response = new();

        response.Result = await _friendService.SendFriendReq(header.Uid, request.FriendId);
        _logger.ZLogInformation($"[SendFriendReq] Uid : {header.Uid}, FriendId : {request.FriendId}");

        return response;

    }
}