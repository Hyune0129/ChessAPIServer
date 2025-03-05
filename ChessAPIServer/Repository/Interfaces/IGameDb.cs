
using APIServer.Models.GameDB;

namespace APIServer.Repository.Interfaces;

public interface IGameDb : IDisposable
{
    Task<int> UpdateLastLoginTime(long uid);
    Task<GdbUserInfo> GetUserByUid(long uid);
    Task<GdbUserInfo> GetUserByPlayerId(long player_id);
    Task<int> InsertUser(long player_id, string nickname);
    Task<int> UpdateUserNickname(long uid, string nickname);
    Task<GdbFriendInfo> GetFriendInfo(long senderUid, long receiverUid);
    Task<int> InsertFriendReq(long senderUid, long receiverUid);
    Task<int> UpdateFriendReq(long senderUid, long receiverUid, bool friendYN);
    Task<int> DeleteFriend(long senderUid, long receiverUid);
    Task<IEnumerable<GdbFriendInfo>> GetFriendListByUid(long uid);
    Task<long> InsertGame(long white_uid, long black_uid);
    Task<GdbGameInfo> GetGameByRoomId(long roomId);
    Task<int> UpdateGameFinsihedByRoomId(long roomId, bool is_finished);
    Task<int> InsertGameMove(long roomId, int count, string piece, string from, string to);

}