using System.Diagnostics;
using Raylib_cs;

class Game {
    public Physics physics;
    public Graphics graphics;

    public Game() {
        physics = new Physics();
        graphics = new Graphics();
    }

    public void Start() {
        bool running = true;

        var physicsTime = Stopwatch.StartNew();
        double lastPhysics = 0;
        Task.Run(() => {
            while(running) {
                var time = Stopwatch.StartNew();
                physics.Tick(lastPhysics);
                lastPhysics = time.Elapsed.TotalSeconds;
            }
        });

        Task.Run(() => {
            var poll = 0.01;
            while(running) {
                if(Raylib.IsKeyDown(KeyboardKey.KEY_ESCAPE))
                    running = false;
                
                // zoom such that the ratio doubles/halves every second
                if(Raylib.IsKeyDown(KeyboardKey.KEY_X))
                    graphics.zoom /= Math.Pow(2, poll);
                if(Raylib.IsKeyDown(KeyboardKey.KEY_Z))
                    graphics.zoom *= Math.Pow(2, poll);
                
                if(Raylib.IsKeyDown(KeyboardKey.KEY_UP))
                    graphics.center.y -= 600 * poll / graphics.zoom;
                if(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
                    graphics.center.y += 600 * poll / graphics.zoom;
                if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT))
                    graphics.center.x -= 600 * poll / graphics.zoom;
                if(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT))
                    graphics.center.x += 600 * poll / graphics.zoom;
                
                Thread.Sleep((int)(poll * 1000));
            }
        });

        // graphics must be run on the main thread
        while(running) {
            graphics.Render(physics, 0);
        }

    }
}