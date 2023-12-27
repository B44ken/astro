// throughout the code, in some files:
// CS8602, dereferencing a possibly null reference, is disabled due to false positives
// CS4104, async function runs asynchrously, is disabled because it is intended behaviour
#pragma warning disable 4014
using System.Drawing;

List<string> arguments = [];
foreach(var arg in Environment.GetCommandLineArgs().ToList()) {
    if(arg[0] != '-') continue;
    arguments.Add(arg);
}

var game = new Game();

var planet = new Entity() {
    canMove = false,
    mass = 1e12,
    radius = 100,
    position = new Vector(0, 0),
    sprite = new Circle(100, Color.Blue),
};

var player = new Astronaut() {
    mass = 100,
    radius = 10,
    position = new Vector(0, -110),
    sprite = new Circle(10, Color.WhiteSmoke),
    input = new Keyboard()
};
game.player = player;

var factory = new ResourceFactory() {
    canMove = false,
    position = new Vector(0, 115),
    sprite = new Square(30, System.Drawing.Color.DarkGray),
    resource = "Iron",
};

game.physics.AddEntity(new List<Entity> {
    planet, player, factory
});

game.Start();