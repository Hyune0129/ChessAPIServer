namespace APIServer;
// 1000 ~ 19999
public enum ErrorCode : UInt16
{
    None = 0,

    // Common 1000 ~
    UnhandleException = 1001,
    RedisFailException = 1002,
    InValidRequestHttpBody = 1003,
    TokenDoesNotExist = 1004,
    UidDoesNotExist = 1005,
    AuthTokenFailWrongAuthToken = 1006,
    LoginServer_Fail_InvalidResponse = 1010,
    LoginServer_Fail_InvalidResponse_Exception = 1011,
    InValidAppVersion = 1012,
    InvalidMasterDataVersion = 1013,

    // Auth 2000 ~
    CreateUserFailException = 2001,
    CreateUserFailNoNickname = 2002,
    CreateUserFailDuplicateNickname = 2003,
    LoginFailException = 2004,
    LoginFailUserNotExist = 2005,
    LoginFailPwNotMatch = 2006,
    LoginFailSetAuthToken = 2007,
    LoginUpdateRecentLoginFail = 2008,
    LoginUpdateRecentLoginFailException = 2009,
    AuthTokenMismatch = 2010,
    AuthTokenKeyNotFound = 2011,
    AuthTokenFailWrongKeyword = 2012,
    AuthTokenFailSetNx = 2013,
    AccountIdMismatch = 2014,
    DuplicatedLogin = 2015,
    CreateUserFailInsert = 2016,
    LoginFailAddRedis = 2017,
    CheckAuthFailNotExist = 2018,
    CheckAuthFailNotMatch = 2019,
    CheckAuthFailException = 2020,
    LogoutRedisDelFail = 2021,
    LogoutRedisDelFailException = 2022,
    DeleteAccountFail = 2023,
    DeleteAccountFailException = 2024,
    InitNewUserGameDataFailException = 2025,
    InitNewUserGameDataFailCharacter = 2026,
    InitNewUserGameDataFailGameList = 2027,
    InitNewUserGameDataFailMoney = 2028,
    InitNewUserGameDataFailAttendance = 2029,
    UpdateUserNickNameFailException = 2030,
    UpdateUserNickNameFailDuplicateNickname = 2031,
    UpdateUserNicknameFail = 2032,

    // Friend 2100
    FriendSendReqFailUserNotExist = 2101,
    FriendSendReqFailInsert = 2102,
    FriendSendReqFailException = 2103,
    FriendSendReqFailAlreadyExist = 2104,
    FriendSendReqFailSameUid = 2105,
    FriendGetListFailOrderby = 2106,
    FriendGetListFailException = 2107,
    FriendGetRequestListFailException = 2108,
    FriendDeleteFailNotFriend = 2109,
    FriendDeleteFailUserNotExist = 2110,
    FriendDeleteFailException = 2111,
    FriendDeleteFailSameUid = 2112,
    FriendAcceptFailException = 2116,
    FriendAcceptFailSameUid = 2117,
    AcceptFriendRequestFailUserNotExist = 2118,
    AcceptFriendRequestFailAlreadyFriend = 2119,
    AcceptFriendRequestFailException = 2120,
    FriendSendReqFailNeedAccept = 2121,

    // Game 2200
    GameBoardInitFailException = 2201,
    GameBoardInitFailCreateRoomFail = 2202,
    GameBoardInitFailCreateGameFail = 2203,
    MovePieceFailNotVaildPlayer = 2204,
    MovePieceFailException = 2205,
    MovePieceFailNotYourTurn = 2206,
    MovePieceFailUpdateRoom = 2207,
    MovePieceFailUpdateGame = 2208,
    MovePieceFailGameNotExist = 2209,
    MovePieceFailRoomNotExist = 2210,
    MovePieceFailInsertMove = 2211,
    WaitTurnFailException = 2212,
    WaitTurnFailGameNotExist = 2213,
    WaitTurnFailRoomNotExist = 2214,
    WaitTurnFailNotVaildPlayer = 2215,
    WaitTurnFailTimeout = 2216,
    SurrenderGameFailException = 2217,
    MovePieceFailNotValidMove = 2218,
    SurrenderGameFailUpdateRoom = 2219,

    GetRankingFailException = 2302,
    GetUserRankFailException = 2303,

    // Item 3000 ~

    //GameDb 4000~ 
    GameDB_Fail_LoadData = 4001,
    GetGameDbConnectionFail = 4002,


    // MasterDb 5000 ~
    MasterDB_Fail_LoadData = 5001,
    MasterDB_Fail_InvalidData = 5002,

    // User
    UserInfoFailException = 6001,
    UserMoneyInfoFailException = 6002,
    UserUpdateJewelryFailIncremnet = 6003,
    SetMainCharFailException = 6004,
    GetOtherUserInfoFailException = 6005,
    UserNotExist = 6006,


    // Mail
    MailListFailException = 8001,
    MailReceiveFailException = 8002,
    MailReceiveFailAlreadyReceived = 8003,
    MailReceiveFailMailNotExist = 8004,
    MailReceiveFailUpdateReceiveDt = 8005,
    MailRewardListFailException = 8006,
    MailDeleteFailDeleteMail = 8007,
    MailDeleteFailDeleteMailReward = 8008,
    MailDeleteFailException = 8009,
    MailReceiveFailNotMailOwner = 8010,
    MailReceiveRewardsFailException = 8011,

    // Attendance
    AttendanceInfoFailException = 9001,
    AttendanceCheckFailAlreadyChecked = 9002,
    AttendanceCheckFailException = 9003,

    GetRewardFailException = 9004,
}