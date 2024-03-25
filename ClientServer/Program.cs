using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientServer
{
    internal class Program
    {

        static void Main(string[] args)
        {
            //Process.Start("Server.exe");
            IPAddress address = Dns.GetHostAddresses(Dns.GetHostName())[2];
            Console.WriteLine("Клиент запущен");

            Console.WriteLine("Введите строку для отправки на сервер:");
            string message = Console.ReadLine();
            IPEndPoint endPoint = new IPEndPoint(address, 1024);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            try
            {
                socket.Connect(endPoint);
                if (socket.Connected)
                {
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    socket.Send(messageBytes);

                    byte[] responseBytes = new byte[1024];
                    int len = socket.Receive(responseBytes);
                    string response = Encoding.UTF8.GetString(responseBytes, 0, len);
                    Console.WriteLine($"Ответ от сервера {socket.LocalEndPoint} данные от клиента получены в {DateTime.Now}");
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
        }
    }
}