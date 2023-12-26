class Astronaut : Entity {
    public Entity? attached;
    Entity? jumpedFrom;
    public double walkSpeed = 100;
    public double jumpSpeed = 100;
    public double walkDirection = 0;
    public bool shouldJump = false;
    public Astronaut() {
        mass = 100;
        sprite = new Circle(15, System.Drawing.Color.White);
    }

    public void Walk(double dt) {
        if(attached == null) return;
        // i have no clue why this errors
        try {
            var rad = walkDirection * walkSpeed * dt / attached.radius;
            position = (position - attached.position).Rotate(rad) + attached.position;
        } catch (NullReferenceException) { }
    }

    public void Jump() {
        var normal = (position - attached.position).Unit();
        position += normal * 0.2;
        velocity += normal * jumpSpeed;
        Console.WriteLine($"Jumped from {attached.position}");
        attached = null;
        canMove = true;
        jumpedFrom = attached;
    }

    public bool Attach(List<Entity> entities) {
        if(attached != null) return false;
        var snap = 0.5;
        foreach(var entity in entities) {
            if(entity == jumpedFrom || entity is Astronaut) continue;
            if((entity.position - position).Size() < entity.radius + radius + snap) {
                velocity = new Vector(0, 0);
                attached = entity;
                canMove = false;
                return true;
            }
        }
        return false;
    }
}