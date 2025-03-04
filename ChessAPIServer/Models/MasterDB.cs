namespace APIServer.Models;

// masterdb : 서버 실행 이후에는 절대 수정 안하는 것들

public class VersionDAO
{
    public string app_version { get; set; } = "";
    public string master_data_version { get; set; } = "";
}