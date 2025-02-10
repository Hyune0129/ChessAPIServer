using APIServer.Models.GameDB;
using APIServer.Services.Friend;

namespace APIServer.Services;

public class FriendService : IFriendService
{
    public Task<ErrorCode> AcceptFriend(long uid, long friendUid)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorCode> DeleteFriend(long uid, long friendUid)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorCode> DenyFriend(long uid, long friendUid)
    {
        throw new NotImplementedException();
    }

    public Task<(ErrorCode, IEnumerable<GdbFriendInfo>)> GetFriendList(long uid)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorCode> SendFriendReq(long uid, long friendUid)
    {
        throw new NotImplementedException();
    }
}