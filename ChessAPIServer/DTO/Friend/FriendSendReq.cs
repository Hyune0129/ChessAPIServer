using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Friend;

public class FriendSendReqRequest
{
    [Required]
    public long FriendId { get; set; }
}

public class FriendSendReqResponse : ErrorCodeDTO
{

}