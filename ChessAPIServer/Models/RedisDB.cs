namespace APIServer.Models;


// redisdb => rdb
public class RdbAuthUserData
{
    public int Uid { get; set; } = 0;
    public string Token { get; set; } = "";
}

public class RdbRoomData
{
    public int game_id { get; set; } = 0;
    public byte[] roomdata { get; set; }

}

public class RdbMoveRecord
{
    public int game_id { set; get; } = 0;
    // move record ref : https://www.chess.com/ko/terms/chess-notation-ko
    public string move_record { get; set; } = "";
}
