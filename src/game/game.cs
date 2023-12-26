using System.Diagnostics;
using Raylib_cs;

class Game {
    public Physics physics = new Physics();
    public Graphics? graphics;
    public GameServer? server;
    public GameClient? client;
    public bool doGraphics = true;

    public Game() {
    }

    public async void Serve() {
        server = new GameServer(this);
        server.Start();
    }

    public async void Connect(string ip) {
        client = new GameClient(this);
        client.Connect(ip);
        client.Listen();
    }

    public void Start() {
        if(doGraphics) graphics = new Graphics();
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
            if(doGraphics) graphics.Render(physics, 0);
        }

    }
}