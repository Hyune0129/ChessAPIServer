using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Game;

public class SurrenderRequest
{
    [Required]
    public long room_id { get; set; }
}
public class SurrenderResponse : ErrorCodeDTO
{

}