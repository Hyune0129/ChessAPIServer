using System.ComponentModel.DataAnnotations;

namespace APIServer.DTO.Game;

public class GameSurrenderRequest
{
    [Required]
    public long room_id { get; set; }
}
public class GameSurrenderResponse : ErrorCodeDTO
{

}