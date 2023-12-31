// throughout the code, in some files:
// CS8602, dereferencing a possibly null reference, is disabled due to false positives
// CS4104, async function runs asynchronously, is disabled because it is intended behaviour
#pragma warning disable 8602
#pragma warning disable 4014
using System.Drawing;


var game = new Game();
// foreach (var i in Enumerable.Range(0, 30)) {
//     var planet = new Entity() {
//         canMove = false,
//         mass = 1e10,
//         radius = 100,
//         position =
//             new Vector(Math.Pow(new Random().Next(10, 100), 2), 0).Rotate(Math.Pow(new Random().Next(0, 360), 2)),
//         sprite = new Circle(100, Color.White)
//     };
//     var closest = 10000.0;
//     game.physics.entities.ForEach(other => {
//         if (other == planet) return;
//         var dist = (other.position - planet.position).Length;
//         if (dist < closest) closest = dist;
//     });
//     if (closest < 300) continue;
//     game.AddEntity(planet);
//     game.physics.Tick(0); // entities are only added on tick
// }

game.AddEntity(new Entity() {
    canMove = false,
    mass = 100,
    radius = 150,
    position = new Vector(0, 200),
    sprite = new Circle(150, Color.White)
});

var player = new Spaceship() {
    radius = 0,
    canMove = false,
    position = new Vector(0, 0),
    velocity = new Vector(0, 0),
    input = new SpaceshipKeyboard(),
    sprite = null,
};

foreach(var I in Enumerable.Range(0, 10)) {
    var i = I - 5;
    game.AddEntity(new Entity() {
        mass = 0.0001,
        radius = 4,
        sprite = new Circle(4, Color.White),
        position = new Vector(i * 20, 0),
        velocity = new Vector(0, 30)
    });
}
    
game.player = player;
game.physics.AddEntity(player);

game.Start();