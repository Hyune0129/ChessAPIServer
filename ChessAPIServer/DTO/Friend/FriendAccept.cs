using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Friend;

public class FriendAcceptRequest
{
    [Required]
    public long FriendId { get; set; }
}

public class FriendAcceptResponse : ErrorCodeDTO
{

}