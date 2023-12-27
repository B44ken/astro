using System.Diagnostics;
using Raylib_cs;

interface Input {
    public bool Pressed(string button);
    public void Update(Astronaut astronaut);
}

class Keyboard : Input {
    Dictionary<string, KeyboardKey> keys = [];
    
    public bool Pressed(string button) {
        return Raylib.IsKeyDown(keys[button]);
    }

    public Keyboard() {
        keys["moveRight"] = KeyboardKey.KEY_A;
        keys["moveLeft"] = KeyboardKey.KEY_D;
        keys["jump"] = KeyboardKey.KEY_W;
        keys["interact"] = KeyboardKey.KEY_SPACE;
        keys["cameraLeft"] = KeyboardKey.KEY_LEFT;
        keys["cameraRight"] = KeyboardKey.KEY_RIGHT;
        keys["cameraUp"] = KeyboardKey.KEY_UP;
        keys["cameraDown"] = KeyboardKey.KEY_DOWN;
        keys["cameraZoomIn"] = KeyboardKey.KEY_Z;
        keys["cameraZoomOut"] = KeyboardKey.KEY_X;
        keys["keyExit"] = KeyboardKey.KEY_ESCAPE;
    }

    public void Update(Astronaut astronaut) {
        if(Pressed("moveRight"))
            astronaut.walkDirection = -1;
        else if(Pressed("moveLeft"))
            astronaut.walkDirection = 1;
        else
            astronaut.walkDirection = 0;

        if(Pressed("jump"))
            astronaut.Jump();
    }
}