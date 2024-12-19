using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Friend;

public class FriendAcceptRequest
{
    [Required]
    public Int64 FriendId { get; set; }
}

public class FriendAcceptResponse : ErrorCodeDTO
{

}