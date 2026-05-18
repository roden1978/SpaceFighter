using System;

public static class Settings
{
    public const int ScreenWidth = 580;
    public const int ScreenHeight = 860;
    public const string GameName = "SPF";
    public static string PlayerName => Environment.UserName;
    public const int LeaderboardCapacity = 5;
    public const float Delay = 2f;
    public const float BackgroundStarsDelay = .5f;
    public const int UpDifficultScoresValue = 500;
    public const float DifficultUpValue = .2f;
}