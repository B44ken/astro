using System.Drawing;
using System.Numerics;

class Entity {
    public double mass = 1;
    public double radius = 10;
    public double rotation = 0;
    public double angularVelocity = 0;
    public HitboxCircle hitbox;
    public Color color = Color.White;
    public Vector position = new Vector(0, 0);
    public Vector velocity = new Vector(0, 0);
    public Vector force = new Vector(0, 0);

    public Entity() {
        hitbox = new HitboxCircle(this);
    }

    public void AddForce(Vector force) {
        this.force += force;
    }

    public void AddImpulse(Vector impulse) {
        this.velocity += impulse / this.mass;
    }

    public void Move(double dt) {
        velocity += force * dt / this.mass;
        position += velocity * dt;
        rotation += angularVelocity * dt;
    }
}