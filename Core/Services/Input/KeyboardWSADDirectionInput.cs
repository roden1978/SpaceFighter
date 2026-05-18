using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class KeyboardWSADDirectionInput : IInputService
{

    public float Jump() { return 0; }
    public Vector2 Move()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            return new(-1, 0);
        else if (Keyboard.GetState().IsKeyDown(Keys.D))
            return new(1, 0);
        else if (Keyboard.GetState().IsKeyDown(Keys.W))
            return new(0, 1);
        else if (Keyboard.GetState().IsKeyDown(Keys.S))
            return new(0, -1);

        return Vector2.Zero;
    }
}
