namespace APIServer.Repository;

public class MemoryDbKeyMaker
{
    const string loginUID = "UID_";
    const string userLockKey = "ULock_";
    const string roomNumberKey = "ROOMNUM_";
    const string gameNumberKey = "GAMENUM_";
    public static string MakeUIDKey(string id)
    {
        return loginUID + id;
    }

    public static string MakeUserLockKey(string id)
    {
        return userLockKey + id;
    }

    public static string MakeRoomNumKey(string id)
    {
        return roomNumberKey + id;
    }

    public static string MakeGameNumKey(string id)
    {
        return gameNumberKey + id;
    }

}