class ResourceFactory : Entity {
    public string resource = "Default";
    public double amount = 1;
    public double range = 50;
    public double cooldown = 1;
    public DateTime lastUsed = DateTime.Now;

    public ResourceFactory() {
        sprite = new Square(20, System.Drawing.Color.Purple);
    }

    public bool Interact(Astronaut user, Game game) {
        var distance = (user.position - position).Length;
        if(distance > range) return false;
        
        var delta = (DateTime.Now - lastUsed).TotalSeconds;
        if(delta < cooldown) return false;
        lastUsed = DateTime.Now;
        
        user.inventory.Add(resource, amount);
        game.Log($"Got {resource}, total: {user.inventory.Get(resource)}");
        return true;
    }
}