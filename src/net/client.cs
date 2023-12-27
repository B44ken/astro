#pragma warning disable 8602

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

class GameClient {
    Socket? socket;
    Game game;

    public GameClient(Game game) {
        this.game = game;
    }

    public bool Connect(string ip) { 
        socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        var localhost = Dns.GetHostEntry(ip).AddressList[0];
        try {
            socket.Connect(localhost, 4153);
            return true;
        } catch(SocketException) {
            return false;
        }
    }

    public void Listen() {
        if(socket == null || !socket.Connected) return;

        while(true) {
            byte[] buffer = new byte[8192];
            int bytesReceived = socket.Receive(buffer);
            string data = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
            List<string> lines = data.Split("\n").ToList();
            foreach(var line in lines) {
                if(line == "" || line == "\r") continue;
                Receive(line);
            }
        }
    }

    public void Receive(string message) {
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(message);
        if(dict == null) return;
        if(dict["type"].ToString() == "entity") {
            var entity = JsonSerializer.Deserialize<Entity>(dict["entity"]?.ToString() ?? "");
            if(entity == null) throw new Exception("Failed to deserialize entity");
            entity = PatchPosition(entity, message);
            entity = PatchVelocity(entity, message);
            if(entity == null) return;
            game.physics.AddEntity(entity);
        }
    }

    public Dictionary<string, object> Traverse(string message, string path) {
        try {
            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(message);
            foreach(var key in path.Split(".")) {
                dict = JsonSerializer.Deserialize<Dictionary<string, object>>(dict[key]?.ToString() ?? "");
            }
            if(dict == null) throw new Exception($"Failed to traverse: {path}");
            return dict;
        } catch(Exception e) {
            throw new Exception($"Failed to traverse: {path}", e);
        }
    }

    public Entity PatchPosition(Entity entity, string message) {
        var dict = Traverse(message, "entity.position.values");
        var vstring = dict["$values"].ToString();
        if(vstring == null) throw new Exception("Failed to patch position");
        var values = vstring.Substring(1, vstring.Length - 2).Split(",");
        entity.position = new Vector(double.Parse(values[0]), double.Parse(values[1]));
        return entity;
    }

    public Entity PatchVelocity(Entity entity, string message) {
        var dict = Traverse(message, "entity.velocity.values");
        var vstring = dict["$values"].ToString();
        var values = vstring.Substring(1, vstring.Length - 2).Split(",");
        entity.velocity = new Vector(double.Parse(values[0]), double.Parse(values[1]));
        return entity;
    }

    public static string Serialize(Entity entity) {
        Dictionary<string, object> dict = new Dictionary<string, object> {
            { "type", "entity" },
            { "entity", entity }
        };

        return JsonSerializer.Serialize(dict, new JsonSerializerOptions {
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve
        });
    }
}