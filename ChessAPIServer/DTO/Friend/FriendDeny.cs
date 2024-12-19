using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Friend;

public class FriendDenyRequest
{
    [Required]
    public Int64 FriendId { get; set; }
}

public class FriendDenyResponse : ErrorCodeDTO
{

}