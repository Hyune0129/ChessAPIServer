namespace APIServer.Services.Interfaces;

public interface IGameService
{
    Task<ErrorCode> MovePiece(int uid, string moveCode);
    Task<ErrorCode> SurrenderGame(int uid);
    Task<ErrorCode> WaitTurn(int game_key);
}