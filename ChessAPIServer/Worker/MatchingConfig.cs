namespace APIServer.Worker;
public class MatchingConfig
{
    public string MatchingFoundRedisAddress { get; set; }
    public string RoomCreatedRedisAddress { get; set; }
    public string PubKey { get; set; }
    public string SubKey { get; set; }
    public string ServerAddress { get; set; }
}