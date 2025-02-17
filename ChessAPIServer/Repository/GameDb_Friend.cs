using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;
using SqlKata.Execution;
using ZLogger;

namespace APIServer.Repository;

public partial class GameDb : IGameDb
{
    public async Task<GdbFriendInfo> GetFriendInfo(long senderUid, long receiverUid)
    {
        GdbFriendInfo friendInfo = await _queryFactory.Query("friend").Where("uid", senderUid).Where("friend_uid", receiverUid).FirstOrDefaultAsync<GdbFriendInfo>();
        _logger.ZLogInformation($"[GameDb.GetFriendInfo] senderUid : {senderUid}, receiverUid : {receiverUid}");
        return friendInfo;
    }
    public async Task<int> InsertFriendReq(long senderUid, long receiverUid)
    {
        return await _queryFactory.Query("friend").InsertAsync(new
        {
            uid = senderUid,
            friend_uid = receiverUid,
        });

    }
    public async Task<int> UpdateFriendReq(long senderUid, long receiverUid, bool friendYN)
    {
        return await _queryFactory.Query("friend")
        .Where("uid", senderUid)
        .Where("friend_uid", receiverUid)
        .UpdateAsync(new
        {
            friend_yn = friendYN
        });
    }
    public async Task<int> DeleteFriend(long senderUid, long receiverUid)
    {
        return await _queryFactory.Query("friend")
        .Where("uid", senderUid)
        .Where("friend_uid", receiverUid)
        .DeleteAsync();
    }
    public async Task<IEnumerable<GdbFriendInfo>> GetFriendListByUid(long uid)
    {
        return await _queryFactory.Query("friend")
        .Where("uid", uid)
        .GetAsync<GdbFriendInfo>();
    }


}