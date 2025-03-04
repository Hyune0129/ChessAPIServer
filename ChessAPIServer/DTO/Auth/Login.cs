using System.ComponentModel.DataAnnotations;
using APIServer.DTO.DataLoad;

namespace APIServer.DTO.Auth;
public class LoginResponse : ErrorCodeDTO
{
    [Required]
    public string Token { get; set; }

    public DataLoadUserInfo userData { get; set; }
}

public class LoginRequest
{
    [Required]
    public Int64 UserId { get; set; }
    [Required]
    public string AccountToken { get; set; }
}