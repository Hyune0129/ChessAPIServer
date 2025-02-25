using APIServer.Models;

namespace APIServer.DTO.Game;

public class TurnWaitResponse : ErrorCodeDTO
{
    public GameDataInfo roomData { get; set; }
}

public class TurnWaitRequest
{
    public long room_id { get; set; }
}

public class GameDataInfo
{
    public RdbRoomData roomData { get; set; }
}
