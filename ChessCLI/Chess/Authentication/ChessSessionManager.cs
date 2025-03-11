using ChessCLI.Authentication;

namespace ChessCLI.Chess.Authentication;

public class ChessSessionManager : SessionManager
{
    public ChessUser currentChessUser;

    public ChessSessionManager()
    {
        currentChessUser = new ChessUser();
    }

}