using System.Diagnostics;

#pragma warning disable 8602
#pragma warning disable 0649

class Physics {
    public List<Entity> entities = new List<Entity>();
    private List<Entity> pendingEntities = new List<Entity>();
    public double G = 1e-4;
    public GameServer? broadcaster;
    
    public void AddEntity(params Entity[] entities) {
        foreach(Entity entity in entities) {
            pendingEntities.Add(entity);
            if(broadcaster != null)
                broadcaster.Broadcast(GameClient.Serialize(entity));
        }
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
                    
                    // wikipedia.org/wiki/Elastic_collision
                    // "Two-dimensional collision with two moving objects"
                    
                    var v1 = entity.velocity;
                    var v2 = other.velocity;
                    var x1 = entity.position;
                    var x2 = other.position;
                    var m1 = entity.mass;
                    var m2 = other.mass;
                    
                    var vf = v1 - 2 * m2 / (m1+m2) * ((v1 - v2) * (x1 - x2))/Math.Pow((x1 - x2).Length, 2) * (x1 - x2);
                    
                    entity.velocity = vf;
                }
            }
        }
    }

    public void Gravity(double dt) {
        foreach (Entity entity in entities) {
            if(!entity.canMove) continue;
            foreach (Entity other in entities) {
                if(entity == other) continue;
                var pos = (other.position - entity.position);
                var dV = dt * G * other.mass / Math.Pow(pos.Length, 2);
                entity.velocity += pos.Unit() * dV;
            }
        }
    }
}