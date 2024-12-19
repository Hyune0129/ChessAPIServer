namespace APIServer.Services;

public class MemoryDbKeyMaker
{
    const string loginUID = "UID_";
    public static string MakeUIDKey(string id)
    {
        return loginUID + id;
    }
}