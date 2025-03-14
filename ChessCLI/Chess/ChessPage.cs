/// <summary>
/// chess server page
/// </summary>
using ChessCLI.Chess.Authentication;

namespace ChessCLI.Chess;
public class ChessPage
{
    readonly ChessService _chessService;
    readonly ChessSessionManager _sessionManager;

    public ChessPage(ChessService chessService)
    {
        _chessService = chessService;
    }

    public void MatchPage()
    {
        int seconds = 0;
        bool cancel = false;

        Task.Run(() =>
        {
            while (true)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.C)
                {
                    cancel = true;
                    break;
                }
            }
        });

        while (!cancel)
        {
            Console.Clear();
            Console.WriteLine("Searching for opponent...");
            Console.WriteLine("Press 'C' to cancel.");
            Console.Write($"Matchmaking... ({seconds:00}:{seconds % 60:00})");
            // todo : match making server 
            Thread.Sleep(1000);
            seconds++;
        }

        Console.WriteLine("Matchmaking canceled.");
    }

    public void FriendPage()
    {
        throw new NotImplementedException();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Friend Page");
            Console.WriteLine("1. Send Friend Request");
            Console.WriteLine("3. Remove Friend");
            Console.WriteLine("4. Back");
            Console.Write("Choice : ");
            int choice = int.Parse(Console.ReadLine());
        }
    }
}