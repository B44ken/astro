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

    public void Connect(string ip) {
        socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        var localhost = Dns.GetHostEntry(ip).AddressList[0];
        socket.Connect(localhost, 4153);
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
        Console.WriteLine("recieved ");
        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(message);
        if(dict == null) return;
        if(dict["type"].ToString() == "entity") {
            Console.WriteLine(dict["entity"].ToString());
            var entity = JsonSerializer.Deserialize<Entity>(dict["entity"]?.ToString() ?? "?");
            if(entity == null) return;
            Console.WriteLine("recieved entity");
            game.physics.AddEntity(entity);
        }
        if(dict["type"].ToString() == "vector") {
            var vector = JsonSerializer.Deserialize<Vector>(dict["vector"]?.ToString() ?? "?");
            if(vector == null) return;
            Console.WriteLine("recieved vector " + vector.ToString());
        }
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