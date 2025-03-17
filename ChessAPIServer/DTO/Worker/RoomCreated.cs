namespace APIServer.DTO.Worker;
public class RoomCreated
{
    public long room_id { get; set; }
    public string server_address { get; set; } = "";
    public int port { get; set; }
}