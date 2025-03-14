using MatchAPIServer;
using MatchAPIServer.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CheckMatching : ControllerBase
{
    IMatchWorker _matchWorker;

    public CheckMatching(IMatchWorker matchWorker)
    {
        _matchWorker = matchWorker;
    }

    [HttpPost]
    public CheckMatchingResponse MatchingCheck(CheckMatchingRequest request)
    {
        CheckMatchingResponse response = new();

        (response.Result, CompleteMatchingData data) = _matchWorker.GetCompleteMatching(request.uid);
        if (response.Result != ErrorCode.None)
        {
            return response;
        }

        response.room_id = data.room_id;
        response.ServerAddress = data.ServerAddress;
        return response;
    }
}

public class CheckMatchingResponse : ErrorCodeDTO
{
    public string ServerAddress { get; set; } = "";
    public int room_id { get; set; }
}

public class CheckMatchingRequest
{
    public long uid { get; set; }
}