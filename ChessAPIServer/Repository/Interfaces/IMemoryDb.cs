using APIServer.Models;

namespace APIServer.Repository.Interfaces;
public interface IMemoryDb
{
    public Task<ErrorCode> RegisterUserAuthAsync(string token, long uid);
    public Task<ErrorCode> CheckUserAuthAsync(string id, string authToken);
    public Task<(bool, RdbAuthUserData)> GetUserAsync(string id);
    public Task<ErrorCode> DeleteUserAuthAsync(long uid);
    public Task<bool> LockUserReqAsync(string key);
    public Task<bool> UnLockUserReqAsync(string key);
    public Task<bool> CreateGameRoomAsync(long roomid);
    public Task<RdbRoomData> GetRoomDataByRoomid(long roomid);

}