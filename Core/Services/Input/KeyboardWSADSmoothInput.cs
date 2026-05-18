using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class KeyboardWSADSmoothInput : IInputService
{
    public float Jump() { return 0; }

    public Vector2 Move()
    {
        int x = 0;
        int y = 0;
        if (Keyboard.GetState().IsKeyDown(Keys.A))
            x = -1;
        if (Keyboard.GetState().IsKeyDown(Keys.D))
            x = 1;
        if (Keyboard.GetState().IsKeyDown(Keys.W))
            y = 1;
        if (Keyboard.GetState().IsKeyDown(Keys.S))
            y = -1;

        return new(x, y);
    }
}
