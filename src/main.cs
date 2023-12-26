#pragma warning disable

using System.Diagnostics;

List<string> arguments = [];
foreach(var arg in Environment.GetCommandLineArgs().ToList()) {
    if(arg[0] != '-') continue;
    arguments.Add(arg);
}

Game game;
if(arguments.Contains("-server")) {
    game = new Game() {
        doGraphics = false
    };
    Task.Run(game.Serve);
    var planetA = new Entity() {
        canMove = false,
        mass = 5e11,
        radius = 100,
        position = new Vector(-150, 0),
        sprite = new Circle(100, System.Drawing.Color.Blue),
    };
    var astro = new Astronaut() {
        position = planetA.position + new Vector(planetA.radius + 20, 0),
        radius = 12,
        sprite = new Circle(12, System.Drawing.Color.White),
    };
    var planetB = new Entity() {
        canMove = false,
        mass = 5e11,
        radius = 50,
        position = new Vector(150, 0),
        sprite = new Circle(50, System.Drawing.Color.Green),
    };
    var keyboard1 = new AstronautKeyboard() {
        parent = astro,
    };

    game.physics.AddEntity(new List<Entity> {
        planetA, planetB, astro
    });

    Task.Run(() => {
        while (true) {
            var old = astro.position;
            double dt = 1.0/60;
            keyboard1.Update();
            astro.Walk(dt);
            var delta = astro.position - old;
            Thread.Sleep((int)(1000 * dt));
        }
    });

    game.Start();
} else {
    game = new Game();
    Task.Run(() => game.Connect("127.0.0.1"));
    Task.Run(() => {
        while(true) {
            Thread.Sleep(200);
            game.graphics?.Log($"n_entities = {game.physics.entities.Count}");
            if(game.physics.entities.Count > 0) {
                Console.WriteLine(game.physics.entities[0].position);
            }
        }
    });
    game.Start();
}