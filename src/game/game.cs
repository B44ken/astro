#pragma warning disable 8602

using System.Diagnostics;
using Raylib_cs;

class Game {
    public Physics physics = new Physics();
    public Graphics? graphics;
    public GameServer? server;
    public GameClient? client;
    public Astronaut? player;
    public bool doGraphics = true;

    public void Serve() {
        server = new GameServer(this);
        server.Start();
    }

    public void Log(string message) {
        graphics?.Log(message);
        Console.WriteLine(message);
    }
    
    public void AddEntity(Entity entity) {
        physics.AddEntity(entity);
    }
    
    public bool Connect(string ip) {
        client = new GameClient(this);
        var res = client.Connect(ip);
        if(res) client.Listen();
        return res;
    }

    public void Start() {
        if(doGraphics) graphics = new Graphics();
        bool running = true;

        // physics
        var physicsTime = Stopwatch.StartNew();
        double lastPhysics = 0;
        Task.Run(() => {
            while(running) {
                var time = Stopwatch.StartNew();
                physics.Tick(lastPhysics);
                lastPhysics = time.Elapsed.TotalSeconds;
            }
        });

        // input
        Task.Run(() => {
            
            var poll = 0.01;
            
            while(running && doGraphics && player != null) {
                player.input.Update(player);
                player.Walk(poll);
                foreach(Entity entity in physics.entities) {
                    if(entity is ResourceFactory resource) {
                        if(!player.input.Pressed("interact")) continue;
                        resource.Interact(player, this);
                    }
                }
                
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
        while(running)
            if(doGraphics) graphics.Render(physics);
    }
}