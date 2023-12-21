var game = new Game();

var planetA = new Entity() {
    canMove = false,
    mass = 5e11,
    radius = 100,
    position = new Vector(-150, 0),
    sprite = new Circle(100, System.Drawing.Color.Blue),
};
var astro = new Astronaut() {
    position = planetA.position + new Vector(planetA.radius + 20, 0),
    radius = 15,
    sprite = new Sprite(15, "c:/dev/astro/assets/astronaut.png")
};
var planetB = new Entity() {
    canMove = false,
    mass = 5e11,
    radius = 50,
    position = new Vector(150, 0),
    sprite = new Circle(50, System.Drawing.Color.Green),
};

game.physics.AddEntity(new List<Entity> {
    planetA, planetB, astro
});

var keyboard1 = new AstronautKeyboard() {
    parent = astro,
};

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