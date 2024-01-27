using System.Diagnostics;
using System.Runtime.InteropServices;

class Astronaut : Entity {
    public Entity? attached;
    public Entity? jumpedFrom;
    public AstronautInput? input;
    public Inventory inventory = new Inventory();
    public double walkSpeed = 100;
    public double jumpSpeed = 150;
    public double walkDirection = 0;

    public Astronaut() {
        mass = 100;
        sprite = new Circle(15, System.Drawing.Color.White);
    }

    public void Walk(double dt) {
        if(attached == null) return;
        try {
            var oldPos = position;
            var rad = walkDirection * walkSpeed * dt / attached.radius;
            position = (position - attached.position).Rotate(rad) + attached.position;
            velocity = (position - oldPos) / dt;
        } catch (NullReferenceException) { }
    }

    public void Jump() {
        if (attached == null) return;
        var normal = (position - attached.position).Unit();
        position += normal * 0.1;
        velocity += normal * jumpSpeed;
        velocity = velocity.Unit() * jumpSpeed;
        attached = null;
        canMove = true;
        jumpedFrom = attached;
    }

    public void Jetpack(double dt) { }

    public bool Attach(List<Entity> entities) {
        if(attached != null) return false;
        var snap = 0.2;
        foreach(var entity in entities) {
            if(entity == jumpedFrom || entity is Astronaut || entity.mass < 10_000) continue;
            var dist = (entity.position - position).Length;
            var rad = entity.radius + radius;
            if(dist - rad < snap) {
                Console.WriteLine("mass = " + entity.mass);
                velocity = new Vector(0, 0);
                attached = entity;
                canMove = false;
                return true;
            }
        }
        return false;
    }
}