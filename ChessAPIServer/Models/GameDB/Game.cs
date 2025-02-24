namespace APIServer.Models.GameDB;

public class GdbGameInfo
{
    public int game_id { get; set; }
    public int white_uid { get; set; }
    public int black_uid { get; set; }
    public bool is_finished { get; set; } = false;
}

public class GdbGameMoveRecord
{
    public int game_id { get; set; }

    public int count { get; set; }
    public string piece { get; set; }
    public string from { get; set; }
    public string to { get; set; }
}