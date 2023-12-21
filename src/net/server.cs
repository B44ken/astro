using System.Net;
using System.Net.Sockets;

class GameServer {
        TcpListener listener;
        List<TcpClient?> clients = [];

        public GameServer() {
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

        public void Start() {
            while (true) {
                var client = listener.AcceptTcpClient();
                clients.Add(client);
                Console.WriteLine("Client connected");
                Broadcast("Client connected");
            }
        }
}