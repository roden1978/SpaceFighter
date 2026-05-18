using System.ComponentModel;
using Microsoft.Xna.Framework;

public class TitleFactory : IFactory<GameObject>
{
    public string Name => GetType().Name;
    private readonly IContentProvider _contentProvider;

    public TitleFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider; 
    }
    public GameObject Create()
    {
        GameObject spaceDrawer = new("SpaceDrawer", new(0, -420), 0, new(1.5f, 1.8f));

        TextDrawer space = new (_contentProvider.FontSequence)
        {
            Text = "Space",
            TextColor = Color.Blue
        };
        spaceDrawer
            .AddComponent(space);

        
        GameObject fighterDrawer = new("FighterDrawer", new(0, 70), -.2f, Vector2.One);
        fighterDrawer.Transform.Parent = spaceDrawer.Transform;
        
        TextDrawer fighter = new (_contentProvider.FontSequence)
        {
            Text = "Fighter",
            TextColor = Color.Magenta
        };
        fighterDrawer
            .AddComponent(fighter);

        return spaceDrawer;
    }
}
