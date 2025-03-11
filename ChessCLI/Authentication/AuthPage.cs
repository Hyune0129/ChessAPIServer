namespace ChessCLI.Authentication;
public class AuthPage
{
    readonly AuthService _authService;
    public AuthPage(AuthService authService)
    {
        _authService = authService;
    }

    public async Task LoginPage()
    {
        while (true)
        {
            Console.WriteLine("Enter email");
            Console.Write("email :");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password");
            Console.Write("password :");
            string password = Console.ReadLine();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Email or password cannot be empty");
                continue;
            }
            LoginServerErrorCode errorCode = await _authService.Login(email, password);
            if (errorCode != LoginServerErrorCode.None)
            {
                PrintLoginError(errorCode);
                continue;
            }

        }
    }

    public void PrintLoginError(LoginServerErrorCode errorCode)
    {
        switch (errorCode)
        {
            case LoginServerErrorCode.LoginFailException:
                Console.WriteLine("Login Failed: Server Error");
                break;
            case LoginServerErrorCode.LoginFailPwNotMatch:
                Console.WriteLine("Login Failed: Password or Email not match");
                break;
            case LoginServerErrorCode.LoginFailUserNotExist:
                Console.WriteLine("Login Failed: Password or Email not match");
                break;
        }
        Console.WriteLine("Press Any Key..");
        Console.ReadKey();
    }

    public async Task RegisterPage()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Enter email");
            Console.Write("email :");
            string email = Console.ReadLine();
            Console.WriteLine("Enter password");
            Console.Write("password :");
            string password = Console.ReadLine();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Email or password cannot be empty");
                continue;
            }
            LoginServerErrorCode errorCode = await _authService.Register(email, password);
            if (errorCode != 0)
            {
                PrintRegisterError(errorCode);
                continue;
            }
            Console.WriteLine("Register Success! Press Any Key..");
            Console.ReadKey();
            break;
        }
    }

    public void PrintRegisterError(LoginServerErrorCode errorCode)
    {
        switch (errorCode)
        {
            case LoginServerErrorCode.CreateAccountFailException:
                Console.WriteLine("Register Failed: Server Error");
                break;
            case LoginServerErrorCode.CreateAccountFailInsert:
                Console.WriteLine("Register Failed: Server Error");
                break;
        }
        Console.WriteLine("Press Any Key..");
        Console.ReadKey();
    }

}