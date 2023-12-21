using System.Net;
using System.Net.Sockets;
using System.Text.Json;
using System.Text.Json.Serialization;

class GameClient {
    Socket? socket;

    public GameClient() {
    }

    public void Connect() {
        socket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        var localhost = Dns.GetHostEntry("localhost").AddressList[0];
        socket.Connect(localhost, 4153);
    }

    public static string SerializeEntity(Entity entity) {
        return JsonSerializer.Serialize(entity, new JsonSerializerOptions {
            WriteIndented = true,
            IncludeFields = true,
            ReferenceHandler = ReferenceHandler.Preserve
        });
    }
}