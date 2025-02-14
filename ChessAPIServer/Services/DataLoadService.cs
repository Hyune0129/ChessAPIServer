using APIServer.DTO.DataLoad;
using APIServer.Repository.Interfaces;
using APIServer.Services.Interfaces;
using ZLogger;

namespace APIServer.Services;

public class DataLoadService : IDataLoadService
{
    readonly IGameDb _gameDb;
    readonly ILogger<DataLoadService> _logger;

    public DataLoadService(IGameDb gameDb, ILogger<DataLoadService> logger)
    {
        _gameDb = gameDb;
        _logger = logger;
    }


    public async Task<(ErrorCode, DataLoadUserInfo)> LoadUserData(long userId)
    {
        DataLoadUserInfo dataLoadUserInfo = new();
        dataLoadUserInfo.UserInfo = await _gameDb.GetUserByUid(userId);
        if (dataLoadUserInfo.UserInfo == null)
        {
            _logger.ZLogError($"[DataLoadService.LoadUserData] uid : {userId}");
            return (ErrorCode.GameDB_Fail_LoadData, null);
        }
        _logger.ZLogInformation($"[DataLoadSErvice.LoadUserData] uid : {userId}");
        return (ErrorCode.None, dataLoadUserInfo);
    }
}