using Raylib_cs;

class Graphics {
    public Vector resolution = new Vector(1600, 900);
    public Vector center = new Vector(0, 0);
    public List<string> logs = new List<string>();
    public Menu? activeMenu;
    public int logLines = 10;
    public double zoom = 3;

    public Graphics() {
        RaylibInit();
    }

    public void Log(string str) {
        logs.Add(str);
        if(logs.Count > logLines)
            logs.RemoveAt(0);
    }

    public void Render(Physics physics) {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.BLACK);

        var entitiesCopy = new List<Entity>(physics.entities);

        foreach(var entity in entitiesCopy) {
            var cameraPos = (entity.position - center) * zoom + resolution / 2;
            if(entity.sprite != null)
                entity.sprite.Draw(cameraPos.x, cameraPos.y, zoom);
        }

        for(int i = 0; i < logs.Count; i++) {
            Raylib.DrawText(logs[i], 10, 10 + 20 * i, 20, Color.WHITE);
        }

        if(activeMenu != null) {
            var text = activeMenu.GetText();
            var width = Raylib.MeasureText(text, 20);
            Raylib.DrawText(text, (int)(resolution.x - width) / 2, 100, 20, Color.WHITE);
        } else {
            // Raylib.DrawText("No active men   u", 100, 100, 20, Color.RED);
        }

        Raylib.EndDrawing();
    }

    public void RaylibInit() {
        Raylib.SetTraceLogLevel(TraceLogLevel.LOG_FATAL);
        Raylib.InitWindow((int)resolution.x, (int)resolution.y, "raylib");
        Raylib.SetTargetFPS(1000);
    }

}