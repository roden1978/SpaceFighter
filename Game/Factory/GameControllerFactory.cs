
public sealed class GameControllerFactory : IFactory<GameObject>
{
    private GameController _gameController;

    public string Name => nameof(GameControllerFactory);
public GameControllerFactory(GameController gameController)
{
    _gameController = gameController;
}
    
    public GameObject Create()
    {
        GameObject gameControllerObject = new("GameController");
        gameControllerObject.AddComponent(_gameController);
        return gameControllerObject;
    }
}