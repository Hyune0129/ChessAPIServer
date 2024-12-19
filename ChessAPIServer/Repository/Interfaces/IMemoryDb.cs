using APIServer.Models;

namespace APIServer.Repository.Interfaces;
public interface IMemoryDb
{
    Task<ErrorCode> RegistUserAuthAsync(string token, int uid);
    Task<ErrorCode> CheckUserAuthAsync(string id, string authToken);
    Task<(bool, RdbAuthUserData)> GetUserAsync(string id);
    Task<ErrorCode> DeleteUserAuthAsync(Int64 uid);


}