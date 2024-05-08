using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace RemoteClient02
{
  internal class Server
  {
    private TcpListener Listener;

    public Server(int Port)
    {
      this.Listener = new TcpListener(IPAddress.Any, Port);
      this.Listener.Start();
      while (true)
        ThreadPool.QueueUserWorkItem(new WaitCallback(Server.ClientThread), (object) this.Listener.AcceptTcpClient());
    }

    private static void ClientThread(object StateInfo)
    {
      Client client = new Client((TcpClient) StateInfo);
    }

    ~Server()
    {
      if (this.Listener == null)
        return;
      this.Listener.Stop();
    }
  }
}
