namespace ChessCLI.Chess.responses.Game;

public class SendCheckMatchingResponse : MatchServerErrorCodeDTO
{
    public string ServerAddress { get; set; } = "";
    public int room_id { get; set; }
}

public class SendCheckMatchingRequest
{
    public long uid { get; set; }
}