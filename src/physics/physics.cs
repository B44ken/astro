using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Physics {
    public List<Entity> entities = new List<Entity>();
    private List<Entity> pendingEntities = new List<Entity>();
    
    public void AddEntity(Entity entity) {
        pendingEntities.Add(entity);
    }

    public void Tick(double dt) {
        foreach (Entity entity in entities) {
            entity.Move(dt);
        }
        Collisions();
        Gravity(dt);
        if(pendingEntities.Count > 0) {
            entities.AddRange(pendingEntities);
            pendingEntities.Clear();
        }
    }

    public void Collisions() {
        foreach (Entity entity in entities) {
            foreach (Entity other in entities) {
                if(entity == other) continue;
                if(entity.hitbox.Collides(other)) {
                    entity.velocity = new Vector(entity.velocity.y * -1, entity.velocity.x);
                }
            }
        }
    }

    public double G = 1e-6;
    public void Gravity(double dt) {
        foreach (Entity entity in entities) {
            foreach (Entity other in entities) {
                if(entity == other) continue;
                var pos = (other.position - entity.position);
                var dV = dt * G * other.mass / Math.Pow(pos.Size(), 2);
                entity.velocity += pos.Normal() * dV;
            }
        }
    }
}