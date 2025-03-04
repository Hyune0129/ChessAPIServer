using APIServer.Controllers.Friend;
using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;
using APIServer.Services.Friend;
using ZLogger;

namespace APIServer.Services;

public class FriendService : IFriendService
{
    readonly ILogger<FriendService> _logger;
    readonly IGameDb _gameDb;
    public FriendService(ILogger<FriendService> logger, IGameDb gameDb)
    {
        _logger = logger;
        _gameDb = gameDb;
    }
    public async Task<ErrorCode> AcceptFriend(long uid, long friendUid)
    {
        try
        {
            GdbUserInfo gdbUserInfo = await _gameDb.GetUserByUid(friendUid);
            // cant find friend
            if (gdbUserInfo is null)
            {
                _logger.ZLogDebug($"[FriendService.AcceptFriend] ErrorCode : {ErrorCode.AcceptFriendRequestFailUserNotExist}");
                return ErrorCode.AcceptFriendRequestFailUserNotExist;
            }

            GdbFriendInfo gdbFriendInfo = await _gameDb.GetFriendInfo(friendUid, uid);
            // dont have req
            if (gdbFriendInfo is null)
            {
                _logger.ZLogDebug($"[FriendService.AcceptFriend] ErrorCode : {ErrorCode.AcceptFriendRequestFailUserNotExist}, uid : {uid}, friend : {friendUid}");
                return ErrorCode.AcceptFriendRequestFailUserNotExist;
            }

            // already friend
            if (gdbFriendInfo.friend_yn is true)
            {
                _logger.ZLogDebug($"[FriendService.AcceptFriend] ErrorCode : {ErrorCode.AcceptFriendRequestFailAlreadyFriend}, uid : {uid}, friend : {friendUid}");
                return ErrorCode.AcceptFriendRequestFailAlreadyFriend;
            }

            // accept
            await _gameDb.UpdateFriendReq(friendUid, uid, true);

        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[FriendService.AcceptFriend] ErrorCode : {ErrorCode.AcceptFriendRequestFailException}, uid : {uid}, friendUid : {friendUid}");
            return ErrorCode.AcceptFriendRequestFailException;
        }
        _logger.ZLogInformation($"[FriendService.AcceptFriend] uid : {uid}, friendUid : {friendUid}");
        return ErrorCode.None;
    }

    /// <summary>
    /// delete / deny request
    /// </summary>
    public async Task<ErrorCode> DeleteFriend(long uid, long friendUid)
    {
        try
        {
            GdbUserInfo gdbUserInfo = await _gameDb.GetUserByUid(friendUid);

            if (gdbUserInfo is null)
            {
                _logger.ZLogDebug($"[FriendService.DeleteFriend] ErrorCode : {ErrorCode.FriendDeleteFailUserNotExist}");
                return ErrorCode.FriendDeleteFailUserNotExist;
            }

            GdbFriendInfo gdbFriendInfo = await _gameDb.GetFriendInfo(uid, friendUid);
            GdbFriendInfo gdbFriendInfo2 = await _gameDb.GetFriendInfo(friendUid, uid);

            // already not friend
            if (gdbUserInfo is null && gdbFriendInfo2 is null)
            {
                _logger.ZLogDebug($"[FriendServ.cei.DeleteFriend] ErrorCode : {ErrorCode.FriendDeleteFailNotFriend}");
                return ErrorCode.FriendDeleteFailNotFriend;
            }

            if (gdbFriendInfo is null)
            {
                gdbFriendInfo = gdbFriendInfo2;
            }

            await _gameDb.DeleteFriend(gdbFriendInfo.uid, gdbFriendInfo.friend_uid);

            return ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[FriendService.DeleteFriend] ErrorCode : {ErrorCode.FriendDeleteFailException}");
            return ErrorCode.FriendDeleteFailException;
        }
    }

    public async Task<(ErrorCode, IEnumerable<GdbFriendInfo>)> GetFriendList(long uid)
    {
        try
        {
            IEnumerable<GdbFriendInfo> friends = await _gameDb.GetFriendListByUid(uid);
            return (ErrorCode.None, friends);
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[FriendService.GetFriendList] Errorcode : {ErrorCode.FriendGetListFailException}");
            return (ErrorCode.FriendGetListFailException, null);
        }
    }

    public async Task<ErrorCode> SendFriendReq(long uid, long friendUid)
    {
        try
        {
            GdbUserInfo gdbUserInfo = await _gameDb.GetUserByUid(friendUid);

            // cant find user
            if (gdbUserInfo is null)
            {
                _logger.ZLogDebug($"[FriendService.SendFriendReq] ErrorCode : {ErrorCode.AcceptFriendRequestFailUserNotExist}, uid : {uid}, friendUid : {friendUid}");
                return ErrorCode.AcceptFriendRequestFailUserNotExist;
            }

            GdbFriendInfo gdbFriendInfo = await _gameDb.GetFriendInfo(uid, friendUid);

            // already friend or already send req
            if (gdbFriendInfo is not null)
            {
                _logger.ZLogDebug($"[FriendService.SendFriendReq] ErrorCode : {ErrorCode.AcceptFriendRequestFailAlreadyFriend}, uid : {uid}, friendUid : {friendUid}");
                return ErrorCode.AcceptFriendRequestFailAlreadyFriend;
            }

            gdbFriendInfo = await _gameDb.GetFriendInfo(friendUid, uid);

            // already friend or already send req (reverse)
            if (gdbFriendInfo is not null)
            {
                _logger.ZLogDebug($"[FriendService.SendFriendReq] ErrorCode : {ErrorCode.AcceptFriendRequestFailAlreadyFriend}, uid : {uid}, friendUid : {friendUid}");
                return ErrorCode.AcceptFriendRequestFailAlreadyFriend;
            }

            await _gameDb.InsertFriendReq(uid, friendUid);
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[FriendService.SendFriendReq] ErrorCode : {ErrorCode.FriendSendReqFailException}");
            return ErrorCode.FriendSendReqFailException;
        }

        return ErrorCode.None;
    }
}