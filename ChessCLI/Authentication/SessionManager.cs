namespace ChessCLI.Authentication;

public class SessionManager
{
    public User currentUser;
    public bool isLogined = false;

    public SessionManager()
    {
        currentUser = new User();
    }

}