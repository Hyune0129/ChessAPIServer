namespace APIServer.Repository;
using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;
using MySqlConnector;
using SqlKata.Execution;

public partial class GameDb : IGameDb
{
    public async Task<GdbUserInfo> GetUserByUid(long uid)
    {
        GdbUserInfo gdbUserInfo = await _queryFactory.Query("user").Where("uid", uid).FirstOrDefaultAsync<GdbUserInfo>();
        return gdbUserInfo;
    }

    public async Task<GdbUserInfo> GetUserByPlayerId(long player_id)
    {
        GdbUserInfo gdbUserInfo = await _queryFactory.Query("user").Where("player_id", player_id).FirstOrDefaultAsync<GdbUserInfo>();
        return gdbUserInfo;
    }

    public async Task<int> UpdateLastLoginTime(long uid)
    {
        var count = await _queryFactory.Query("user").Where("uid", uid).UpdateAsync(new { recent_login_dt = DateTime.Now });
        return count;
    }


    public async Task<int> InsertUser(long player_id, string nickname)
    {
        int count = await _queryFactory.Query("user").InsertAsync(new
        {
            player_id = player_id,
            nickname = nickname
        });
        return count;
    }

    public async Task<int> UpdateUserNickname(long uid, string nickname)
    {
        int count = await _queryFactory.Query("user")
        .Where("uid", uid).UpdateAsync(new
        {
            nickname = nickname
        });
        return count;
    }
}