using Raylib_cs;

class AstronautKeyboard {
    public Astronaut? parent;
    Dictionary<string, KeyboardKey> keys = [];

    public AstronautKeyboard() {
        keys["right"] = KeyboardKey.KEY_A;
        keys["left"] = KeyboardKey.KEY_D;
    }

    public void Update() {
        if(parent == null)
            return;

        if(Raylib.IsKeyDown(keys["right"]))
            parent.walkDirection = -1;
        else if(Raylib.IsKeyDown(keys["left"]))
            parent.walkDirection = 1;
        else
            parent.walkDirection = 0;

        if(Raylib.IsKeyDown(KeyboardKey.KEY_W) && parent.attached != null) {
            parent.Jump();
        }
    }
}