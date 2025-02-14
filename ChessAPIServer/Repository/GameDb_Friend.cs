using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;
using SqlKata.Execution;

namespace APIServer.Repository;

public partial class GameDb : IGameDb
{
    public async Task<GdbFriendInfo> GetFriendInfo(long senderUid, long receiverUid)
    {
        try
        {
            GdbFriendInfo friendInfo = await _queryFactory.Query("friend").Where("uid", senderUid).Where("friend_uid", receiverUid).FirstOrDefaultAsync<GdbFriendInfo>();

        }
        catch (Exception ex)
        {

        }
    }
    public Task<int> InsertFriendReq(long senderUid, long receiverUid)
    {

        throw new NotImplementedException();
    }
    public Task<int> UpdateFriendReq(long senderUid, long receiverUid, bool friendYN)
    {
        throw new NotImplementedException();
    }
    public Task<int> DeleteFriend(long senderUid, long receiverUid)
    {

        throw new NotImplementedException();
    }
    public Task<IEnumerable<GdbFriendInfo>> GetFriendListByUid(long uid)
    {

        throw new NotImplementedException();
    }


}