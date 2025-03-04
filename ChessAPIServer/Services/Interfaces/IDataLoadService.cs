
using APIServer.DTO.DataLoad;

namespace APIServer.Services.Interfaces;

public interface IDataLoadService
{
    Task<(ErrorCode, DataLoadUserInfo)> LoadUserData(long userId);
}