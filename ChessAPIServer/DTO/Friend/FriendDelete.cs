using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Friend;

public class FriendDeleteRequest
{
    [Required]
    public long friendUid { get; set; }
}

public class FriendDeleteResponse : ErrorCodeDTO
{

}