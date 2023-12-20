using System.Diagnostics;
using System.Numerics;

interface Hitbox {
    
}

class HitboxCircle : Hitbox {
    public Entity parent;

    public HitboxCircle(Entity parent) {
        this.parent = parent;
    }

    public bool Collides(Entity otherEntity) {
        var other = otherEntity.hitbox as HitboxCircle;
        if (other == null) return false;
        var dist = (parent.position - other.parent.position).Size();
        var minDist = (parent.radius + other.parent.radius);

        return dist < minDist;
    }
}