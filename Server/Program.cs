using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];                             
            IPEndPoint endPoint = new IPEndPoint(address, 1024);
            Socket pass_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            pass_socket.Bind(endPoint);
            pass_socket.Listen(10);
            Console.WriteLine($"Сервер начал прослушивание на порту 1024");
            
            try
            {
                int len;
                while(true)
                {
                                       
                    Socket socket = pass_socket.Accept();                  
                    Console.WriteLine($"Подключился хост {socket.LocalEndPoint}");
                    Console.WriteLine("Hello");
                    Console.WriteLine($"Подключился хост {socket.LocalEndPoint}");                    
                    byte[] buf = new byte[1024];
                    int received = socket.Receive(buf);
                    string message = Encoding.UTF8.GetString(buf, 0, received);
                    Console.WriteLine($"{message}");
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
            catch (SocketException ex)
            {

                Console.WriteLine(ex);
            } 
        }
    }
}