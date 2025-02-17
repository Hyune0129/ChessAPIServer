using APIServer.Repository.Interfaces;
using APIServer.Services.Interfaces;
using MySqlConnector;
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

    public async Task<ErrorCode> CreateUser(long uid, string nickname)
    {
        try
        {
            int count = await _gameDb.InsertUser(uid, nickname);
            if (count != 1)
            {
                _logger.ZLogDebug($"[UserService.CreateUser] Errorcode : {ErrorCode.CreateUserFailInsert}, uid : {uid}, nickname : {nickname}");
                return ErrorCode.CreateUserFailInsert;
            }
            return ErrorCode.None;
        }
        catch (MySqlException e) when (e.Number == (int)MySqlErrorCode.DuplicateKey)
        {
            _logger.ZLogDebug($"[UserService.CreateUser] ErrorCode : {ErrorCode.CreateUserFailDuplicateNickname}, uid : {uid}, nickname : {nickname}");
            return ErrorCode.CreateUserFailDuplicateNickname;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[UserService.CreateUser] ErrorCode : {ErrorCode.CreateUserFailDuplicateNickname}, uid : {uid}, nickname : {nickname}");
            return ErrorCode.CreateUserFailException;
        }

    }

    public async Task<ErrorCode> ChangeNickname(long uid, string nickname)
    {
        try
        {
            int count = await _gameDb.UpdateUserNickname(uid, nickname);
            if (count != 1)
            {
                _logger.ZLogError($"[UserService.ChangeNickname] ErrorCode : {ErrorCode.UpdateUserNicknameFail}, uid : {uid}, nickname : {nickname}");
                return ErrorCode.UpdateUserNicknameFail;
            }
            _logger.ZLogInformation($"[UserService.ChangeNickname] uid : {uid}, nickname : {nickname}");
            return ErrorCode.None;
        }
        catch (MySqlException ex) when (ex.Number == (int)MySqlErrorCode.DuplicateKey)
        {
            _logger.ZLogDebug($"[UserService.ChangeNickname] ErrorCode : {ErrorCode.UpdateUserNickNameFailDuplicateNickname}, uid : {uid}, nickname : {nickname}");
            return ErrorCode.UpdateUserNickNameFailDuplicateNickname;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[UserService.ChangeNickName] ErrorCode : {ErrorCode.UpdateUserNickNameFailException}, uid : {uid}, nickname : {nickname}");
            return ErrorCode.UpdateUserNickNameFailException;
        }
    }


}