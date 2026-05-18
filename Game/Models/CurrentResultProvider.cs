public sealed class CurrentResultProvider : IScoreStringProvider, IResultProvider
{
    public StringSource StringSource {get;}
    public Result Result {get;}
    public CurrentResultProvider()
    {
        Result = new()
        {
            PlayerName = Settings.PlayerName,
            Game = Settings.GameName
        };

        StringSource = new() { Value = "0" };
    }

}