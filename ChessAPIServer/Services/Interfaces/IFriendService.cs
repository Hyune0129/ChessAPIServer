using APIServer.Models.GameDB;

namespace APIServer.Services.Friend;
public interface IFriendService
{
    Task<ErrorCode> AcceptFriend(Int64 uid, Int64 friendUid);
    Task<ErrorCode> SendFriendReq(Int64 uid, Int64 friendUid);
    Task<ErrorCode> DenyFriend(Int64 uid, Int64 friendUid);
    Task<ErrorCode> DeleteFriend(Int64 uid, Int64 friendUid);
    Task<(ErrorCode, IEnumerable<GdbFriendInfo>)> GetFriendList(Int64 uid);
}