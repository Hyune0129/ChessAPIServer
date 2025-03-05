using System.ComponentModel.DataAnnotations;
using APIServer.DTO.DataLoad;

namespace APIServer.DTO.Auth;
public class LoginResponse : ErrorCodeDTO
{
    [Required]
    public DataLoadUserInfo userData { get; set; }
}

public class LoginRequest
{
    [Required]
    public long player_id { get; set; }
    [Required]
    public string AccountToken { get; set; }
}