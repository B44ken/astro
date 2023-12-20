using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using Raylib_cs;

var game = new Game();

game.graphics.logLines = 2;

var earth1 = new Entity() {
    mass = 1,
    radius = 100,
    position = new Vector(-200, 0),
    color = System.Drawing.Color.Blue,
};
var astro1 = new Astronaut() {
    position = earth1.position - new Vector(0, earth1.radius + 10),
    attachedTo = earth1,
};

var earth2 = new Entity() {
    mass = 1,
    radius = 50,
    position = new Vector(200, 0),
    color = System.Drawing.Color.Blue,
};
var astro2 = new Astronaut() {
    position = earth2.position - new Vector(0, earth2.radius + 10),
    attachedTo = earth2,
};

game.physics.AddEntity(earth1);
game.physics.AddEntity(earth2);
game.physics.AddEntity(astro1);
game.physics.AddEntity(astro2);

Task.Run(() => {
    while (true) {
        var old = new Vector[2] { astro1.position, astro2.position };
        double dt = 1.0/60;
        astro1.walkRight(dt);
        astro2.walkRight(dt);
        var delta = new Vector[2] { astro1.position - old[0], astro2.position - old[1] };
        game.graphics.Log($"astro1: {delta[0].Size() / dt}");
        game.graphics.Log($"astro2: {delta[1].Size() / dt}");
        Thread.Sleep(1000/60);
    }
});

game.StartLoop();