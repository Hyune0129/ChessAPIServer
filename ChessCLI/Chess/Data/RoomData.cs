namespace ChessCLI.Chess.Data;

public class RoomData
{
    public string white_uid { get; set; }
    public string black_uid { get; set; }
    public int turnCount { get; set; } = 0;


    // "White", "Black"
    public string turn { get; set; } = "White";
    public string last_move { get; set; }
    public string game_status { get; set; }
    public string winner { get; set; }
    public bool is_check { get; set; }
    public bool is_checkmate { get; set; }
}