class Spaceship : Entity {
    public double maxThrust = 10;
    public double thrustForce = 0;
    public double fuel = 100;
    public double fuelConsumption = 1;
    public double rotateControl = 0;
    public SpaceshipInput? input;
    
    public void Thrust(double dt) {
        if(thrustForce == 0) return;
        var force = new Vector(1, 0).Rotate(rotation) * thrustForce * dt;
        velocity += force / mass;
        fuel -= fuelConsumption * dt * thrustForce / maxThrust;
    }
    
    public void Rotate(double dt) {
        rotation += angularVelocity * dt * rotateControl;
    }
}