using System;
using Microsoft.Xna.Framework;

public class VerticalAmplifier : Component
{
    private Vector2 _startPosition;

    public VerticalAmplifier(Vector2 startPosition = new())
    {
        _startPosition = startPosition;
    }
    public override void Update(GameTime gameTime)
    {
        if (Active)
            gameObject.Transform.Position = new Vector2(gameObject.Transform.Position.X,
                _startPosition.Y + MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds * 3) * 20);
    }

    public void UpdateStartPosition(Vector2 newPosition) => _startPosition = newPosition;
}
