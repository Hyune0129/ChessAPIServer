using APIServer.Repository.Interfaces;
using APIServer.Services.Interfaces;

namespace APIServer.Services;

public class GameService : IGameService
{
    ILogger<GameService> _logger;
    IMemoryDb _memoryDb;

    public GameService(IMemoryDb memoryDb, ILogger<GameService> logger)
    {
        _memoryDb = memoryDb;
        _logger = logger;
    }

    public Task<ErrorCode> MovePiece(int uid, string moveCode)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorCode> SurrenderGame(int uid)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorCode> WaitTurn(int game_key)
    {
        throw new NotImplementedException();
    }
}