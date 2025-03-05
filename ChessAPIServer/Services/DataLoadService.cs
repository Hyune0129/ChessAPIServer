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


    public async Task<(ErrorCode, DataLoadUserInfo)> LoadUserData(long player_id)
    {
        DataLoadUserInfo dataLoadUserInfo = new();
        dataLoadUserInfo.UserInfo = await _gameDb.GetUserByPlayerId(player_id);
        if (dataLoadUserInfo.UserInfo == null)
        {
            _logger.ZLogError($"[DataLoadService.LoadUserData] player_id : {player_id}");
            return (ErrorCode.GameDB_Fail_LoadData, null);
        }
        _logger.ZLogInformation($"[DataLoadSErvice.LoadUserData] uid : {dataLoadUserInfo.UserInfo.uid}, player_id : {player_id}");
        return (ErrorCode.None, dataLoadUserInfo);
    }
}