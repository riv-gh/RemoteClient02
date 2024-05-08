using System;

namespace RemoteClient02
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Server server = new Server(3080);
    }
  }
}
