public class StartScreenBehaviour : Component, IStart, ICanvasComponent
{
    public Button StartButton;
    private readonly IPersistentProgressService _persistentProgressService;

    public StartScreenBehaviour(IPersistentProgressService persistentProgressService)
    {
        _persistentProgressService = persistentProgressService;
    }

    public override void Start()
    {
        _persistentProgressService.Load();
    }
}
