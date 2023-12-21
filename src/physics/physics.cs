using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;

class Physics {
    public List<Entity> entities = new List<Entity>();
    private List<Entity> pendingEntities = new List<Entity>();
    public GameServer? broadcaster;
    
    public void AddEntity(Entity entity) {
        pendingEntities.Add(entity);
        if(broadcaster != null) {
            broadcaster.Broadcast(GameClient.SerializeEntity(entity));
        }
    }

    public void AddEntity(List<Entity> entities) {
        pendingEntities.AddRange(entities);
    }

    public void Tick(double dt) {
        foreach (Entity entity in entities) {
            entity.Move(dt);
            if(entity is Astronaut) {
                ((Astronaut) entity).Walk(dt);
                ((Astronaut) entity).Attach(entities);
            }
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
                    if(entity is Astronaut && (entity as Astronaut).attached == other)
                        continue;

                    // elastic collision accounting for mass and velocity
                    var normal = (other.position - entity.position).Normal();
                    var v1 = entity.velocity;
                    var v2 = other.velocity;
                    var m1 = entity.mass;
                    var m2 = other.mass;
                    var v1f = v1 - 2 * m2 / (m1 + m2) * (v1 - v2) * (normal) * normal;
                    var v2f = v2 - 2 * m1 / (m1 + m2) * (v2 - v1) * (normal) * normal;
                    entity.velocity = v1f;
                    other.velocity = v2f;
                }
            }
        }
    }

    public double G = 1e-6;
    public void Gravity(double dt) {
        foreach (Entity entity in entities) {
            if(!entity.canMove) continue;
            foreach (Entity other in entities) {
                if(entity == other) continue;
                var pos = (other.position - entity.position);
                var dV = dt * G * other.mass / Math.Pow(pos.Size(), 2);
                entity.velocity += pos.Normal() * dV;
            }
        }
    }
}