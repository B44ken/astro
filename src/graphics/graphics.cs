using Raylib_cs;

class Graphics {
    public Vector resolution = new Vector(1600, 900);
    public Vector center = new Vector(0, 0);
    public List<string> logs = new List<string>();
    public int logLines = 5;
    public double zoom = 3;

    public Graphics() {
        RaylibInit();
    }

    public void Log(string str) {
        logs.Add(str);
        if(logs.Count > logLines)
            logs.RemoveAt(0);
    }

    public void Render(Physics physics, double dt) {
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.BLACK);

        var entitiesCopy = new List<Entity>(physics.entities);

        // foreach(var entity in entitiesCopy) {
        //     var color = new Color(entity.color.R, entity.color.G, entity.color.B, entity.color.A);
        //     var cameraPos = (entity.position - center) * zoom + resolution / 2;
        //     Raylib.DrawCircle((int)cameraPos.x, (int)cameraPos.y, (float)(entity.radius * zoom), color);
        // }

        foreach(var entity in entitiesCopy) {
            var cameraPos = (entity.position - center) * zoom + resolution / 2;
            if(entity.sprite != null)
                entity.sprite.draw(cameraPos.x, cameraPos.y, zoom);
        }

        for(int i = 0; i < logs.Count; i++) {
            Raylib.DrawText(logs[i], 10, 10 + 20 * i, 20, Color.WHITE);
        }

        Raylib.EndDrawing();
    }

    public void RaylibInit() {
        Raylib.InitWindow((int)resolution.x, (int)resolution.y, "raylib");
        Raylib.SetTargetFPS(144);
    }

}