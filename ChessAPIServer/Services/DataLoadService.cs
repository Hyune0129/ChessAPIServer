using APIServer.DTO.DataLoad;
using APIServer.Services.Interfaces;

namespace APIServer.Services;

public class DataLoadService : IDataLoadService
{
    public Task<(ErrorCode, DataLoadUserInfo)> LoadUserData(long userId)
    {
        throw new NotImplementedException();
    }
}