// in some files:
// CS8602, dereferencing a possibly null reference, is disabled due to false positives
// CS4104, async function runs asynchronously, is disabled because it is intended behaviour
#pragma warning disable 8602
#pragma warning disable 4014

using System.Drawing;

var game = new Game();
game.Init();

var player = new Astronaut() {
    mass = 10,
    position = new Vector(0, -100),
    input = new AstronautKeyboard(),
    sprite = new Circle(12, Color.Red)
};

var iron = new ResourceFactory() {
    resource = "Iron",
    radius = 6,
    position = new Vector(56, 0),
    sprite = new Square(12, Color.Gray),
    canMove = false
};

var planet = new Entity() {
    radius = 50,
    mass = 1e10,
    sprite = new Circle(50, Color.Blue),
    canMove = false
};

game.graphics.activeMenu = new Menu() {
    title = "Main menu",
    options = new List<string>() {
        "Start",
        "Exit"
    }
};

Console.WriteLine("Hello world!");

Task task = Task.Run(() => {
    while(true) {
        // game.Log("Player position: " + player.position);
        Thread.Sleep(200);
    }
});

game.player = player;
game.physics.AddEntity([player, iron, planet]);
game.Start();