using System.Drawing;

class Entity {
    public double mass = 1;
    public double radius = 10;
    public double rotation = 0;
    public double angularVelocity = 0;
    public bool canMove = true;
    public HitboxCircle hitbox;
    public Drawable sprite = new Circle(10, Color.White);
    public Vector position = new Vector(0, 0);
    public Vector velocity = new Vector(0, 0);
    public Input input;

    public Entity() {
        hitbox = new HitboxCircle(this);
    }

    public void Impulse(Vector impulse) {
        this.velocity += impulse / this.mass;
    }

    public void Move(double dt) {
        position += velocity * dt;
        rotation += angularVelocity * dt;
    }
}