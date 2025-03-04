using APIServer.Models.GameDB;

namespace APIServer.Services.Friend;
public interface IFriendService
{
    Task<ErrorCode> AcceptFriend(long uid, long friendUid);
    Task<ErrorCode> SendFriendReq(long uid, long friendUid);
    Task<ErrorCode> DeleteFriend(long uid, long friendUid);
    Task<(ErrorCode, IEnumerable<GdbFriendInfo>)> GetFriendList(long uid);
}