using APIServer.Models.GameDB;

namespace APIServer.DTO.DataLoad;
public class UserDataLoadResponse : ErrorCodeDTO
{
    public DataLoadUserInfo UserData { get; set; }
}

public class DataLoadUserInfo
{
    public GdbUserInfo UserInfo { get; set; }
}