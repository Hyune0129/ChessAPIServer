namespace APIServer.Repository;
using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;
using SqlKata.Execution;
using ZLogger;

public partial class GameDb : IGameDb
{
    public async Task<GdbUserInfo> GetUserByUid(long uid)
    {
        try
        {
            GdbUserInfo gdbUserInfo = new();
            gdbUserInfo = await _queryFactory.Query("user").Where("uid", uid).FirstOrDefaultAsync<GdbUserInfo>();
            _logger.ZLogInformation($"[GameDb.GetUserByUid] uid : {uid}");
            return gdbUserInfo;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[GameDb.GetUserByUid] uid : {uid} ErrorCode : {ErrorCode.UserInfoFailException}");
            return null;
        }
    }

    public async Task<int> UpdateLastLoginTime(long uid)
    {
        try
        {
            var count = await _queryFactory.Query("user").Where(uid).UpdateAsync(new { recent_login_dt = DateTime.Now });
            _logger.ZLogInformation($"[GameDb.UpdateLastLoginTime] uid : {uid}");
            return count;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[GameDb.UpdateLastLoginTime] uid : {uid} ErrorCode : {ErrorCode.LoginUpdateRecentLoginFailException}");
            return -1;
        }
    }
}