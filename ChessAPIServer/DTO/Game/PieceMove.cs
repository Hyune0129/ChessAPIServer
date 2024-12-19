namespace APIServer.DTO.Game;
public class PieceMoveResponse : ErrorCodeDTO
{

}

public class PieceMoveRequest
{
    public string move_code { get; set; }
}