using System.ComponentModel.DataAnnotations;

namespace APIServer.Model.DTO;

public class VerifyTokenRequest
{
    [Required]
    public Int64 PlayerId { get; set; }
    [Required]
    public string Token { get; set; }
}

public class VerifyTokenResponse : ErrorCodeDTO
{

}