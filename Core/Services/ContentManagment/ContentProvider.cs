using System;
using System.Collections.Immutable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ContentProvider : IContentProvider
{
    private readonly IContentLoadService _contentLoadService;
    public Sequence FontSequence => _fontSequence;
    private Sequence _fontSequence;
    private Sequence _meteorBig;
    private ImmutableDictionary<TextureTypes, Texture2D> _textures;
    private ImmutableDictionary<AccessTypes, string> _accessStrings;
    private Random _random;

    public ContentProvider(IContentLoadService contentLoadService)
    {
        _contentLoadService = contentLoadService;
        _random = new();
    }

    public void LoadAll()
    {
        _textures = _contentLoadService.LoadConvertedTextures();
        _accessStrings = _contentLoadService.LoadConvertedAccessDBStrings();
        _fontSequence = new Sequence(new SpriteOrder(_textures[TextureTypes.Font], Point.Zero, 10, 9, new Rectangle(0, 0, 480, 414)));
        _meteorBig = new Sequence(new SpriteOrder(_textures[TextureTypes.MeteorBig], 8, 1));
        _contentLoadService.CreateDefaultTexture2D();
    }

    public Sprite GetRandomMeteorBig() => 
        _meteorBig[_random.Next(0, _meteorBig.Length)];

    public Texture2D GetTextureByType(TextureTypes type)
    {
        if (_textures.TryGetValue(type, out Texture2D value))
            return value;

        throw new ArgumentException($"Texture type {type} is not exist!");
    }

    public string GetAccessStringByType(AccessTypes type)
    {
        if (_accessStrings.TryGetValue(type, out string value))
            return value;

        throw new ArgumentException($"Texture type {type} is not exist!");
    }

    public int GetRandomIndex() =>
        _random.Next(0, 1);

    public void Start() => 
        LoadAll();

    public Sprite CreateDefaultSprite(int width = 32, int height = 32, Color color = new Color()) => 
        _contentLoadService.CreateDefaultSprite(width, height, color);

    public Sprite GenerateBackground()
    {
        int backgroundSize = Settings.ScreenWidth * Settings.ScreenHeight;
        Color[] background = new Color[backgroundSize];
        for (int i = 0; i < backgroundSize; i++)
        {
            background[i] = _random.Next(0, 100) > 98 ? Color.White : Color.Black;
        }

        Texture2D tex = _contentLoadService.CreateDefaultTexture2D(Settings.ScreenWidth, Settings.ScreenHeight);

        tex.SetData(background);

        return new Sprite(tex);
    }

}
