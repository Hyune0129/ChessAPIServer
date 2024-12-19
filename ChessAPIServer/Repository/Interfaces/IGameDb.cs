
using APIServer.Models.GameDB;

namespace APIServer.Repository.Interfaces;

public interface IGameDb
{
    Task<int> UpdateLastLoginTime(long uid);
    Task<GdbUserInfo> GetUserByUid(long uid);
}