using System.Security.Cryptography.X509Certificates;
using APIServer.Models;

namespace APIServer.Repository.Interfaces;
public interface IMemoryDb
{
    public Task<ErrorCode> RegisterUserAuthAsync(string token, long uid);
    public Task<ErrorCode> CheckUserAuthAsync(string id, string authToken);
    public Task<(bool, RdbAuthUserData)> GetUserAsync(string id);
    public Task<ErrorCode> DeleteUserAuthAsync(long uid);
    public Task<bool> LockUserReqAsync(string key);
    public Task<bool> UnLockUserReqAsync(string key);
    public Task<(bool, int)> GetRoomNumber(long uid);
    public Task<bool> CreateRoom(long roomid, long white_uid, long black_uid);
    public Task<bool> CreateGame(long roomid);
    public Task<RdbRoomData> GetRoomDataByRoomid(long roomid);
    public Task<RdbGameData> GetGameDataByRoomid(long roomid);
    public Task<bool> UpdateRoomData(long roomid, string turn, int turnCount, string last_move);
    public Task<bool> UpdateGameData(long roomid, byte[] board);
    public Task<string> GetTurnByRoomid(long roomid);

}