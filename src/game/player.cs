using System.Diagnostics;
using System.Drawing;

class Astronaut : Entity {
    public Entity attachedTo;
    public double walkSpeed = 100;

    public Astronaut() {
        mass = 100;
        color = Color.Red;
    }

    public void walkRight(double dt) {
        if(attachedTo == null) return;
        var newPos = (position - attachedTo.position).Rotate(walkSpeed * dt / attachedTo.radius) + attachedTo.position;
        position = newPos;
    }

    public void walkLeft(double dt) {
        walkRight(-dt);
    }
}