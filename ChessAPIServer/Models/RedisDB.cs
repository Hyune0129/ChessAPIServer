namespace APIServer.Models;
// redisdb => rdb


public class RdbAuthUserData
{
    public long Uid { get; set; } = 0;
    public string Token { get; set; } = "";
}

// redis hashmap
public class RdbRoomData
{
    public string white_uid { get; set; }
    public string black_uid { get; set; }
    public int turnCount { get; set; } = 0;

    // White / Black
    public string turn { get; set; } = "White"; // first turn is white

    // move record ref : https://www.chess.com/ko/terms/chess-notation-ko
    public string last_move { get; set; } = "";

    public string game_status { get; set; } = "InProgress"; // InProgress, Surrendered, Finished
    public string winner { get; set; } = ""; // White, Black, Draw
    public bool is_check { get; set; } = false;
    public bool is_checkmate { get; set; } = false;

}


// redis string
public class RdbGameData
{
    // W(white) / B(black) + K / Q / B / N / R / P
    public byte[] board { get; set; }
}

