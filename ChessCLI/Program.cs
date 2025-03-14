using ChessCLI.Authentication;
using ChessCLI.Chess;

SessionManager sessionManager;
AuthService authService;
AuthPage authPage;

init();

while (true)
{
    LoginMenu();
}

void init()
{
    sessionManager = new SessionManager();
    authService = new AuthService(sessionManager);
    authPage = new AuthPage(authService);
}

async void LoginMenu()
{
    while (!sessionManager.isLogined)
    {
        Console.Clear();
        Console.WriteLine("1.Login");
        Console.WriteLine("2.Register");
        Console.WriteLine("3.exit");
        Console.Write("Choice : ");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                await authPage.LoginPage();
                MainMenu();
                break;
            case 2:
                await authPage.RegisterPage();
                break;
            case 3:
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice Press Any Key to continue");
                Console.ReadKey();
                break;
        }
    }

}

async void MainMenu()
{
    while (sessionManager.isLogined)
    {
        Console.Clear();
        Console.WriteLine("==Game Menu==");
        Console.WriteLine("1.Chess Game");
        Console.WriteLine("2.Logout");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                ChessMenu();
                break;
            case 2:
                await authService.Logout();
                break;
            default:
                Console.WriteLine("Invalid choice Press any key to continue");
                Console.ReadKey();
                break;
        }
    }
}

async void ChessMenu()
{
    ChessService chessService = new ChessService(sessionManager);
    ChessPage chessPage = new ChessPage(chessService);
    while (true)
    {
        Console.Clear();
        Console.WriteLine("==Chess Menu==");
        Console.WriteLine("1.Match Room");
        Console.WriteLine("2.Friend");
        Console.WriteLine("3.Exit");
        int choice = int.Parse(Console.ReadLine());
        switch (choice)
        {
            case 1:
                chessPage.MatchPage();
                break;
            case 2:
                chessPage.FriendPage();
                break;
            case 3:
                return;
            default:
                Console.WriteLine("Invalid choice");
                break;
        }
    }
}


// another games..
// async void AnotherGameMenu()
// {
//     AnotherGameService anotherService = new AnotherGameService(sessionManager);
//     AnotherGamePage anoterGamePage = new AnotherGamePagge(AnotherGameService);
//     while (true)
//     {
//         Console.Clear();
//         Console.WriteLine("==Menu==");
//         Console.WriteLine("1.Match Room");
//         Console.WriteLine("2.Friend");
//         Console.WriteLine("3.Exit");
//         int choice = int.Parse(Console.ReadLine());
//         switch (choice)
//         {
//             case 1:
//                 anotherGamePage.MatchPage();
//                 break;
//             case 2:
//                 anotherGamePage.FriendPage();
//                 break;
//             case 3:
//                 return;
//             default:
//                 Console.WriteLine("Invalid choice");
//                 break;
//         }
//     }
// }