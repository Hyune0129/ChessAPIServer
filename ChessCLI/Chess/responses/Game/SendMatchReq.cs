namespace ChessCLI.Chess.responses.Game;
public class SendMatchReqResponse : MatchServerErrorCodeDTO
{
    long room_id { get; set; }
}

public class SendMatchReqRequest
{
    public long uid { get; set; }
}