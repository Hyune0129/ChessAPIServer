
using APIServer.Models.GameDB;

namespace APIServer.Repository.Interfaces;

public interface IGameDb : IDisposable
{
    Task<int> UpdateLastLoginTime(long uid);
    Task<GdbUserInfo> GetUserByUid(long uid);
    Task<int> InsertUser(long uid, string nickname);
    Task<int> UpdateUserNickname(long uid, string nickname);
    Task<GdbFriendInfo> GetFriendInfo(long senderUid, long receiverUid);
    Task<int> InsertFriendReq(long senderUid, long receiverUid);
    Task<int> UpdateFriendReq(long senderUid, long receiverUid, bool friendYN);
    Task<int> DeleteFriend(long senderUid, long receiverUid);
    Task<IEnumerable<GdbFriendInfo>> GetFriendListByUid(long uid);

}