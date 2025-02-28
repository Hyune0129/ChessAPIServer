using APIServer.Domain.Pieces;
using Humanizer.Bytes;

namespace APIServer.Domain;

public class Board
{
    Piece[,] board = new Piece[8, 8];
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
}