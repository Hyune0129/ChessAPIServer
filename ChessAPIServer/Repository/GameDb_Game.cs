using APIServer.Models.GameDB;
using APIServer.Repository.Interfaces;
using SqlKata.Execution;

namespace APIServer.Repository;

public partial class GameDb : IGameDb
{

    /// <summary>
    /// make gameRoom and return room_id (long)
    /// </summary>
    public async Task<long> InsertGame(long white_uid, long black_uid)
    {
        long room_id = await _queryFactory.Query("game_room").InsertGetIdAsync<long>(new
        {
            white_uid = white_uid,
            black_uid = black_uid,
            is_finished = false,
        });
        return room_id;
    }

    public async Task<GdbGameInfo> GetGameByRoomId(long roomId)
    {
        GdbGameInfo game = await _queryFactory.Query("game_room").Where("game_id", roomId).FirstAsync<GdbGameInfo>();
        return game;
    }
    public async Task<int> UpdateGameFinsihedByRoomId(long roomId, bool is_finished)
    {
        int count = await _queryFactory.Query("game_room").Where("game_id", roomId).UpdateAsync(new
        {
            is_finished = is_finished
        });
        return count;
    }

    public async Task<int> InsertGameMove(long roomId, int count, string piece, string from, string to)
    {
        int insert_count = await _queryFactory.Query("game_move_record").InsertAsync(new
        {
            game_id = roomId,
            count = count,
            piece = piece,
            from = from,
            to = to
        });
        return insert_count;
    }

}