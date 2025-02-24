using APIServer.Models;
using APIServer.Repository;
using APIServer.Repository.Interfaces;
using APIServer.Services.Interfaces;
using Microsoft.OpenApi.Extensions;
using MySqlConnector;
using StackExchange.Redis;
using ZLogger;

namespace APIServer.Services;

public class GameService : IGameService
{
    ILogger<GameService> _logger;
    IMemoryDb _memoryDb;
    IGameDb _gameDb;

    public GameService(IMemoryDb memoryDb, ILogger<GameService> logger, IGameDb gameDb)
    {
        _memoryDb = memoryDb;
        _logger = logger;
        _gameDb = gameDb;
    }


    public async Task<(ErrorCode, long)> GameInit(long player1_id, long player2_id, bool is_randomTeam)
    {
        long white_uid;
        long black_uid;

        // randomize team
        if (is_randomTeam)
        {
            Random random = new();
            if (random.Next(10) % 2 == 0)
            {
                white_uid = player1_id;
                black_uid = player2_id;
            }
            else
            {
                white_uid = player2_id;
                black_uid = player1_id;
            }
        }
        else
        {
            white_uid = player1_id;
            black_uid = player2_id;
        }
        try
        {
            long room_id = await _gameDb.InsertGame(white_uid, black_uid);
            if (!await _memoryDb.CreateRoom(room_id, white_uid, black_uid))
            {
                _logger.ZLogDebug($"[GameService.GameInit] ErrorCode : {ErrorCode.GameBoardInitFailCreateRoomFail}");
                return (ErrorCode.GameBoardInitFailCreateRoomFail, -1);
            }

            if (!await _memoryDb.CreateGame(room_id))
            {
                _logger.ZLogDebug($"[GameService.GameInit] ErrorCode : {ErrorCode.GameBoardInitFailCreateGameFail}");
                return (ErrorCode.GameBoardInitFailCreateGameFail, -1);
            }

            return (ErrorCode.None, room_id);
        }
        catch (MySqlException ex)
        {
            _logger.ZLogError(ex, $"[GameService.GameInit] Error : MysqlConnection error");
            return (ErrorCode.GameBoardInitFailException, -1);
        }
        catch (RedisException ex)
        {
            _logger.ZLogError(ex, $"[GameService.GameInit] Error : RedisConnection error");
            return (ErrorCode.GameBoardInitFailException, -1);
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[GameService.GameInit] Error : DB_Connection error");
            return (ErrorCode.GameBoardInitFailException, -1);
        }

    }

    public async Task<ErrorCode> MovePiece(long uid, long room_id, string piece, string from, string to)
    {
        try
        {
            RdbRoomData room = await _memoryDb.GetRoomDataByRoomid(room_id);
            RdbGameData game = await _memoryDb.GetGameDataByRoomid(room_id);
            Team team;
            Team turn = (Team)Enum.Parse(typeof(Team), room.turn);

            if (room is null)
            {
                _logger.ZLogDebug($"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailRoomNotExist}");
                return ErrorCode.MovePieceFailRoomNotExist;
            }

            if (game is null)
            {
                _logger.ZLogDebug($"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailGameNotExist}");
                return ErrorCode.MovePieceFailGameNotExist;
            }

            if (room.black_uid.Equals(uid.ToString()))
            {
                // player is black
                team = (Team)Enum.Parse(typeof(Team), "Black");
            }
            else if (room.white_uid.Equals(uid.ToString()))
            {
                // player is white
                team = (Team)Enum.Parse(typeof(Team), "White");
            }
            else
            {
                // not player
                _logger.ZLogDebug($"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailNotVaildPlayer}");
                return ErrorCode.MovePieceFailNotVaildPlayer;
            }

            // is my turn?
            if (team != turn)
            {
                _logger.ZLogDebug($"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailNotYourTurn}");
                return ErrorCode.MovePieceFailNotYourTurn;
            }


            // room update data
            if (!await _memoryDb.UpdateRoomData(room_id, GetNextTurn(turn).GetDisplayName(), room.turnCount + 1, to))
            {
                _logger.ZLogDebug($"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailUpdateRoom}");
                return ErrorCode.MovePieceFailUpdateRoom;
            }
            if (game.board is null)
            {
                return ErrorCode.MovePieceFailGameNotExist;
            }

            // game update data
            UpdateBoard(game.board, from, to);

            if (!await _memoryDb.UpdateGameData(room_id, game.board))
            {
                _logger.ZLogDebug($"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailUpdateGame}");
                return ErrorCode.MovePieceFailUpdateGame;
            }

            if (await _gameDb.InsertGameMove(room_id, room.turnCount + 1, piece, from, to) != 1)
            {
                _logger.ZLogDebug($"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailUpdateGame}");
                return ErrorCode.MovePieceFailInsertMove;
            }

            return ErrorCode.None;
        }
        catch (Exception ex)
        {
            _logger.ZLogError(ex, $"[GameService.MovePiece] uid : {uid}, room_id : {room_id}, piece : {piece}, from : {from}, to : {to}, ErrorCode : {ErrorCode.MovePieceFailException}");
            return ErrorCode.MovePieceFailException;
        }
    }

    public Task<ErrorCode> SurrenderGame(long uid, long room_id)
    {
        throw new NotImplementedException();
    }

    // todo : find and implement long polling
    public Task<ErrorCode> WaitTurn(long uid, long room_id)
    {
        throw new NotImplementedException();
    }

    Team GetNextTurn(Team currentTurn)
    {
        if (Team.White == currentTurn)
        {
            return Team.Black;
        }
        else if (Team.Black == currentTurn)
        {
            return Team.White;
        }
        return Team.None;
    }

    void UpdateBoard(byte[] board, string from, string to)
    {
        int fromIdx = getBoardIndex(from);
        int toIdx = getBoardIndex(to);
        byte piece = board[fromIdx];
        board[fromIdx] = 0;
        board[toIdx] = piece;
    }

    int getBoardIndex(string pos)
    {
        // a8 b8 c8 d8 e8 f8 g8 h8
        // a7 b7 c7 d7 e7 f7 g7 h7
        // a6 b6 c6 d6 e6 f6 g6 h6
        // a5 b5 c5 d5 e5 f5 g5 h5
        // a4 b4 c4 d4 e4 f4 g4 h4
        // a3 b3 c3 d3 e3 f3 g3 h3
        // a2 b2 c2 d2 e2 f2 g2 h2
        // a1 b1 c1 d1 e1 f1 g1 h1

        int row = 8 - (pos[1] - '0');
        int col = pos[0] - 'a';

        return (row * 8) + col;
    }

}