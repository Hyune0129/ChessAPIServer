using ChessCLI.Chess.Data;

namespace ChessCLI.Chess.responses.Game;

public class TurnWaitResponse : ChessServerErrorCodeDTO
{
    RoomData roomData { get; set; }
}

