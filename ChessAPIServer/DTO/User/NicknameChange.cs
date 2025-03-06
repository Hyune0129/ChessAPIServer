using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.User;

public class NicknameChangeRequest
{
    [Required]
    public string Nickname { get; set; }
}

public class NicknameChangeResponse : ErrorCodeDTO
{

}