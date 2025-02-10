using APIServer.Models;

namespace APIServer.Repository.Interfaces;
public interface IMemoryDb
{
    public Task<ErrorCode> RegistUserAuthAsync(string token, int uid);
    public Task<ErrorCode> CheckUserAuthAsync(string id, string authToken);
    public Task<(bool, RdbAuthUserData)> GetUserAsync(string id);
    public Task<ErrorCode> DeleteUserAuthAsync(Int64 uid);
    public Task<bool> LockUserReqAsync(string key);
    public Task<bool> UnLockUserReqAsync(string key);

}