using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

public class KeyboardVerticalStepInput : IInputService
{
    private bool _isUp;
    private bool _isDown;
    public float Jump() { return 0; }
    public Vector2 Move()
    {
        if (Keyboard.GetState().IsKeyDown(Keys.W) && !_isUp)
        {
            _isUp = true;
            return new(0, 1);
        }
        else
            if (Keyboard.GetState().IsKeyDown(Keys.S) && !_isDown)
        {
            _isDown = true;
            return new(0, -1);
        }

        if (Keyboard.GetState().IsKeyUp(Keys.W) && _isUp)
        {
            _isUp = false;
        }
        else 
            if (Keyboard.GetState().IsKeyUp(Keys.S) && _isDown)
        {
            _isDown = false;
        }

        return Vector2.Zero;
    }
}
