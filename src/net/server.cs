using System.Net;
using System.Net.Sockets;

class GameServer {
        Game game;
        TcpListener listener;
        List<TcpClient?> clients = [];

        public GameServer(Game game) {
            this.game = game;
            listener = new TcpListener(new IPAddress(0), 4153);
            listener.Start();
        }

        public void Broadcast(string message) {
            clients.RemoveAll(client => client == null);

            for(int i = 0; i < clients.Count; i++) {
                try {
                    var writer = new StreamWriter(clients[i].GetStream()) {
                        AutoFlush = true
                    };
                    writer.WriteLine(message);
                } catch (IOException) {
                    clients[i] = null;
                }
            }
        }

        public void BroadcastTo(TcpClient client, string message) {
            try {
                var writer = new StreamWriter(client.GetStream()) {
                    AutoFlush = true
                };
                writer.WriteLine(message);
            } catch (IOException) {}
        }

        public void Start() {
            while (true) {
                var client = listener.AcceptTcpClient();
                clients.Add(client);
                var entities = game.physics.entities;
                foreach (var entity in game.physics.entities) {
                    BroadcastTo(client, GameClient.Serialize(entity) + "");
                }
            }
        }
}