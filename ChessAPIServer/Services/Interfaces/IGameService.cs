using APIServer.DTO.Game;

namespace APIServer.Services.Interfaces;

public interface IGameService
{
    Task<(ErrorCode, long)> GameInit(long player1_id, long player2_id, bool is_randomTeam);
    Task<ErrorCode> MovePiece(long uid, long room_id, string piece, string from, string to);
    Task<ErrorCode> SurrenderGame(long uid, long room_id);
    Task<(ErrorCode, GameDataInfo)> WaitTurn(long uid, long room_id);
}