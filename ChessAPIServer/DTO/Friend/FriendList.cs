using APIServer.Models.GameDB;

namespace APIServer.DTO.Friend;

public class FriendListResponse : ErrorCodeDTO
{
    public IEnumerable<GdbFriendInfo> FriendList { get; set; }
}