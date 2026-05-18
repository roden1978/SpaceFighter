using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class InstructionLabelFactory : IFactory<GameObject>
{
    private readonly IContentProvider _contentProvider;

    public InstructionLabelFactory(IContentProvider contentProvider)
    {
        _contentProvider = contentProvider;
    }

    public string Name => GetType().Name;

    public GameObject Create()
    {
        Texture2D instructionLabel = _contentProvider.GetTextureByType(TextureTypes.Instruction);

        GameObject instructionLabelDrawer = new("InstructionDrawer", new(0, 350), 0, Vector2.One);

        instructionLabelDrawer
            .AddComponent(new UIImage(new Sprite(instructionLabel)));

        return instructionLabelDrawer;
    }
}
