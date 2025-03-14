using MatchAPIServer;
using MatchAPIServer.DTO;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class RequestMatching : ControllerBase
{
    IMatchWorker _matchWorker;

    public RequestMatching(IMatchWorker matchWorker)
    {
        _matchWorker = matchWorker;
    }

    [HttpPost]
    public MatchingResponse MatchingRequest(MatchingRequestRequest request)
    {
        MatchingResponse response = new();

        _matchWorker.AddUser(request.uid);

        return response;
    }

}


public class MatchingRequestRequest
{
    public long uid { get; set; }
}
public class MatchingResponse : ErrorCodeDTO
{

}