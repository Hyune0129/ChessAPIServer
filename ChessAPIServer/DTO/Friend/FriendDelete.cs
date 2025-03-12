using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Friend;

public class FriendDeleteRequest
{
    [Required]
    public long FriendUid { get; set; }
}

public class FriendDeleteResponse : ErrorCodeDTO
{

}