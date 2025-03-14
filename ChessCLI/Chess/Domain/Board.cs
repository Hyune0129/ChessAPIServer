using ChessCLI.Chess.Domain;
using ChessCLI.Chess.Domain.Pieces;

namespace APIServer.Chess.Domain;
public class Board
{
    public Piece[,] board = new Piece[8, 8];
    public (int, int) whiteKingPos;
    public (int, int) blackKingPos;

    public Board(byte[] byteBoard)
    {
        byte teamMask = 0xF0;
        byte pieceMask = 0x0F;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Team team = (Team)((byteBoard[i * 8 + j] & teamMask) >> 4);
                PieceType pieceType = (PieceType)(byteBoard[i * 8 + j] & pieceMask);
                if (pieceType == PieceType.King)
                {
                    if (team == Team.White)
                    {
                        whiteKingPos = (i, j);
                    }
                    else
                    {
                        blackKingPos = (i, j);
                    }
                }
                board[i, j] = Piece.GetPieceByPieceType(pieceType, i, j, team);
            }
        }
    }

    public bool IsMoveValid(string from, string to)
    {
        int fromRow, fromCol, toRow, toCol;
        (fromRow, fromCol) = ParsePosition(from);
        (toRow, toCol) = ParsePosition(to);
        if (fromRow < 0 || fromRow > 7 || fromCol < 0 || fromCol > 7 || toRow < 0 || toRow > 7 || toCol < 0 || toCol > 7)
        {
            return false;
        }

        // from pos is empty
        if (board[fromRow, fromCol].pieceType == PieceType.None)
        {
            return false;
        }

        // invaild piece move
        if (!board[fromRow, fromCol].MoveCheck(toRow, toCol))
        {
            return false;
        }

        // catch
        if (board[fromRow, fromCol].team != board[toRow, toCol].team)
        {
            return true;
        }

        return false;
    }

    private (int, int) ParsePosition(string position)
    {
        int row = position[1] - '1';
        int col = position[0] - 'a';
        return (row, col);
    }

    public void RotateBoard()
    {
        Piece[,] rotatedBoard = new Piece[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                rotatedBoard[i, j] = board[7 - i, 7 - j];
            }
        }
        board = rotatedBoard;

        whiteKingPos = (7 - whiteKingPos.Item1, 7 - whiteKingPos.Item2);
        blackKingPos = (7 - blackKingPos.Item1, 7 - blackKingPos.Item2);
    }
}