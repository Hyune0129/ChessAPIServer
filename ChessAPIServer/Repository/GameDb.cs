using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;

namespace APIServer.Repository;

public class GameDb : IGameDb
{
    // todo : gamedb implement
    public Task<GdbUserInfo> GetUserByUid(long uid)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateLastLoginTime(long uid)
    {
        throw new NotImplementedException();
    }
}