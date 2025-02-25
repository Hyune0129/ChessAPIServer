namespace APIServer.DTO.Game;
public class PieceMoveResponse : ErrorCodeDTO
{

}

public class PieceMoveRequest
{
    public long room_id { get; set; }
    public string from { get; set; }
    public string to { get; set; }
    public string piece { get; set; }
}