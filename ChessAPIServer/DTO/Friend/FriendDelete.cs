using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Friend;

class FriendDeleteRequest
{
    [Required]
    long friendUid { get; set; }
}

class FriendDeleteResponse : ErrorCodeDTO
{

}