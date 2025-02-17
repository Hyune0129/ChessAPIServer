using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.User;

public class UserCreateRequest
{
    [Required]
    public long Uid { get; set; }
    [Required]
    public string Nickname { get; set; }
}
public class UserCreateResponse : ErrorCodeDTO
{

}

