// throughout the code, in some files:
// CS8602, dereferencing a possibly null reference, is disabled due to false positives
// CS4104, async function runs asynchrously, is disabled because it is intended behaviour
#pragma warning disable 8602
#pragma warning disable 4014
using System.Drawing;

List<string> arguments = [];
foreach(var arg in Environment.GetCommandLineArgs().ToList()) {
    if(arg[0] != '-') continue;
    arguments.Add(arg);
}

var game = new Game();

foreach(int i in Enumerable.Range(0, 30)) {
    var planet = new Entity() {
        canMove = false,
        mass = 1e10,
        radius = 100,
        position = new Vector(Math.Pow(new Random().Next(10, 100), 2), 0)
            .Rotate(Math.Pow(new Random().Next(0, 360), 2)),
        sprite = new Circle(100, Color.White),
    };
    var closest = 10000.0;
    foreach(var other in game.physics.entities) {
        if(other == planet) continue;
        var dist = (other.position - planet.position).Length;
        if(dist < closest) closest = dist;
    }
    if(closest < 300) continue;
    game.AddEntity(planet);
    game.physics.Tick(0); // entities are only added on tick
}

var player = new Astronaut() {
    radius = 10,
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

var factoryText = new Entity() {
    canMove = false,
    position = factory.position - new Vector(25, 40),
    sprite = new Text() {
        text = "Iron Deposit"
    }
};

Task.Run(() => {
    while(true) {
        if((player.position - factory.position).Length < 50) {
            ((Text) factoryText.sprite).text = "Iron Deposit";
        }
        else {
            ((Text) factoryText.sprite).text = "";
        }
        Task.Delay(100);
    }
});

game.physics.AddEntity(player, factory, factoryText);

game.Start();