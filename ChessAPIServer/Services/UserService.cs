using APIServer.Repository.Interfaces;
using APIServer.Services.Interfaces;
using ZLogger;

namespace APIServer.Services;

public class UserService : IUserService
{
    readonly ILogger<UserService> _logger;
    readonly IGameDb _gameDb;
    readonly IMemoryDb _memoryDb;

    public UserService(ILogger<UserService> logger, IGameDb gameDb, IMemoryDb memoryDb)
    {
        _logger = logger;
        _gameDb = gameDb;
        _memoryDb = memoryDb;
    }

    public Task<ErrorCode> CreateNickname(long uid, string nickname)
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorCode> ChangeNickname(long uid, string nickname)
    {
        throw new NotImplementedException();
    }


}