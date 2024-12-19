namespace APIServer.DTO.Game;

public class TurnWaitResponse : ErrorCodeDTO
{

}

public class TurnWaitRequest
{
    public int game_key { get; set; }
}