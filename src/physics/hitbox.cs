interface Hitbox {
    bool Collides(Entity otherEntity);
}

class HitboxCircle : Hitbox {
    public Entity parent;

    public HitboxCircle(Entity parent) {
        this.parent = parent;
    }

    public bool Collides(Entity other) {
        if(other.hitbox is HitboxCircle) {
            if (other == null) return false;
            var dist = (parent.position - other.position).Size();
            var minDist = parent.radius + other.radius;
            return dist < minDist;
        }
        else return false;
    }
}